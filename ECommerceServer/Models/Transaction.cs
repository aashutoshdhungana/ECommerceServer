using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceServer.Models
{
    public class Transaction
    {
        [Key]
        public Guid TrasanctionId { get; set; }

        [Required]
        [ForeignKey("User")]
        public Guid PayerId { get; set; }

        [Required]
        [ForeignKey("User")]
        public Guid PayeeId { get; set; }

        [ForeignKey("Order")]
        public Guid OrderId { get; set; }

        [Required]
        public DateTime TransactionDateTime { get; set; }

        [Required]
        public double Amount { get; set; }
        [Required]
        public string Remarks { get; set; }
    }
}
