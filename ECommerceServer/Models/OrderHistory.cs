using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceServer.Models
{
    public class OrderHistory
    {
        [Key]
        public Guid OrderHistoryId { get; set; }
        public ICollection<Order> Orders { get; set; }
        [ForeignKey("User")]
        public Guid UserId { get; set; }
    }
}
