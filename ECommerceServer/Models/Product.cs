using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ECommerceServer.Models.ViewModel;

namespace ECommerceServer.Models
{
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Product Name")]
        public string ProductName { get; set; }

        [DisplayName("Product Type")]
        public string Type { get; set; }

        [Required]
        [DisplayName("Quantity in stock")]
        public uint Quantity { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [DisplayName("Price")]
        public double Price { get; set; }

        [MaxLength(750)]
        [DisplayName("Product Description")]
        public string Description { get; set; }

        public string Image { get; set; }

        [DisplayName("Days For Delivery")]
        public int DeliveryDays { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public UserViewModel User { get; set; }

    }
}
