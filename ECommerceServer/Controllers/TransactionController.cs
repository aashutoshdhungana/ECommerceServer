using ECommerceServer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using ECommerceServer.Models;
using System;
using System.Linq;
using ECommerceServer.Models.Enumerations;

namespace ECommerceServer.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IOrderHistoryService _orderHistoryService;
        private readonly IOrderService _orderService;
        private readonly IWalletService _walletService;
        private readonly ITransactionService _transactionService;
        private readonly IProductService _productService;
        public TransactionController(IOrderHistoryService orderHistoryService, IOrderService orderService, IWalletService walletService, ITransactionService transactionService, IProductService productService)
        {
            _walletService = walletService;
            _orderHistoryService = orderHistoryService;
            _orderService = orderService;
            _transactionService = transactionService;
            _productService = productService;
        }
        
        [HttpPost]
        [Route("/Order/Place")]
        public async Task<IActionResult> PlaceOrder([FromBody] Order order)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Product related
                    Product product = await _productService.GetProductByIdAsync(order.ProductId);
                    var quantityAvailable = product.Quantity;
                    var payerId = order.UserId;
                    var payeeId = product.UserId;
                    if (quantityAvailable <= order.Quantity)
                    {
                        product.Quantity -= order.Quantity;
                        return BadRequest("Product not in stock");
                    }
                    product.Quantity -= order.Quantity;

                    // Order related 
                    order.DeliveryDate = DateTime.Now.AddDays(product.DeliveryDays);
                    order.OrderPlacementTime = DateTime.Now;
                    order.Status = OrderStatus.DELEVERING;
                    await _orderService.CreateOrderAsync(order);

                    // Order History related
                    OrderHistory orderHistory = await _orderHistoryService.GetOrderHistoryByUserIdAsync(order.UserId);
                    if (orderHistory == null)
                    {
                        orderHistory = new OrderHistory
                        {
                            Orders = { order },
                            UserId = order.UserId,
                        };
                        await _orderHistoryService.CreateOrderHistoryAsync(orderHistory);
                    }
                    else
                    {
                        orderHistory.Orders.Add(order);
                        _orderHistoryService.UpdateOrderHistory(orderHistory);
                    }

                    // Transaction related
                    Transaction tranasaction = new Transaction
                    {
                        PayerId = payerId,
                        PayeeId = payeeId,
                        OrderId = order.OrderId,
                        TransactionDateTime = DateTime.Now,
                        Amount = order.TotalPrice,
                        Remarks = "Payed for product",
                    };

                    // Wallet related
                    if (!await _walletService.PerformTransactionAsync(tranasaction))
                    {
                        return BadRequest("Transaction failed");
                    }

                   
                    _productService.UpdateProduct(product);
                    await _transactionService.CreateTransactionAsync(tranasaction);
                   
                    // Save all changes
                    await _productService.SaveChangeAsync();
                    await _orderService.saveChangesAsync();
                    await _orderHistoryService.SaveChangesAsync();
                    await _transactionService.SaveChangesAsync();
                    return Ok("Order Placed Successfully");
                }
                catch (Exception)
                {
                    return StatusCode(500);
                }
            }

            else
            {
                var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return BadRequest(message);
            }
        }
        [HttpPost]
        [Route("/Order/History/{id:guid}")]
        public async Task<IActionResult> ViewOrderHistory(Guid id)
        {
            return Ok(await _orderHistoryService.GetOrderHistoryByUserIdAsync(id));
        }

        [HttpPost]
        [Route("/Order/Get/{id:Guid}")]
        public async Task<IActionResult> ViewOrder(Guid id)
        {
            Order order = await _orderService.GetOrderAsync(id);
            return Ok(order);
        }
    }
}
