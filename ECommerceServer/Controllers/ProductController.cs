using System;
using System.Linq;
using AutoMapper;
using ECommerceServer.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ECommerceServer.Services;
using Microsoft.AspNetCore.Authorization;
using ECommerceServer.Models.ViewModel;
using ECommerceServer.Authorization;

namespace ECommerceServer.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        public ProductController(IMapper mapper, IProductService productService, IUserService userService, IAuthorizationService authorizationService)
        {
            _productService = productService;
            _mapper = mapper;
            _userService = userService;
            _authorizationService = authorizationService;
        }

        [HttpPost]
        [Route("/Product/Create")]
        public async Task<IActionResult> CreateProductAsync( [FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                var isAuthorized = await _authorizationService.AuthorizeAsync(User, product, AuthorizationOperations.Create);
                if (!isAuthorized.Succeeded)
                {
                    return Forbid();
                }
                await _productService.CreateProductAsync(product);
                await _productService.SaveChangeAsync();
                return Ok("Created product successfully");
            }

            else
            {
                var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return BadRequest(message);
            }    
        }
        
        [HttpPut]
        [Route("/Product/Update/{id:guid}")]
        public async Task<IActionResult> UpdateProductAsync(Guid id, [FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var validProduct = await _productService.GetProductByIdAsync(id);
                    if (validProduct == null)
                        return NotFound();
                    var isAuthorized = await _authorizationService.AuthorizeAsync(User, validProduct, AuthorizationOperations.Update);
                    if (!isAuthorized.Succeeded)
                    {
                        return Forbid();
                    }
                    var productModel = _mapper.Map(product, validProduct);
                    _productService.UpdateProduct(productModel);
                    await _productService.SaveChangeAsync();
                    return Ok("Product Updated Successfully");
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

        [HttpDelete]
        [Route("/Product/Delete/{id:guid}")]
        public async Task<IActionResult> DeleteProductAsync(Guid id) 
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var validProduct = await _productService.GetProductByIdAsync(id);
                    if (validProduct == null)
                        return NotFound();

                    var isAuthorized = await _authorizationService.AuthorizeAsync(User, validProduct, AuthorizationOperations.Delete);
                    if (!isAuthorized.Succeeded)
                    {
                        return Forbid();
                    }

                    _productService.DeleteProduct(validProduct);
                    await _productService.SaveChangeAsync();
                    return Ok("Product Deleted Successfully");
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
        [Route("/Product/{id:guid}")]
        public async Task<IActionResult> GetProductAsync(Guid id)
        {
            try
            {
                Product product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound();
                }

                var isAuthorized = await _authorizationService.AuthorizeAsync(User, product, AuthorizationOperations.Read);
                if (!isAuthorized.Succeeded)
                {
                    return Forbid();
                }

                User user = await _userService.GetUserByIdAsync(product.UserId);
                product.User = _mapper.Map<UserViewModel>(user);
                return Ok(product);
            }

            catch (Exception)
            {
                return StatusCode(500);
            }
       }

        [HttpPost]
        [Route("/UserProducts/{id:guid}")]
        public IActionResult GetProductByUserId(Guid id)
        {
            return Ok(_productService.GetProductsByUserId(id));
        }
    }
}
