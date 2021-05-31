using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ECommerceServer.Models.Enumerations;

namespace ECommerceServer.Models
{
    public class Order
    {
        [Key]
        [DisplayName("Order Id")]
        public Guid OrderId { get; set; }

        [Required]
        public Guid ProductId { get; set; }
        
        [Required]
        public uint Quantity { get; set; }

        [DataType(DataType.Currency)]
        [DisplayName("Total")]
        public double TotalPrice{ get; set; }

        [DisplayName("Status")]
        public OrderStatus Status { get; set; }
        public DateTime OrderPlacementTime { get; set; }
        public DateTime DeliveryDate { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }
    }
}
