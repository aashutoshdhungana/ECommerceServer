using System;
using System.ComponentModel.DataAnnotations;

namespace ECommerceServer.Models.ViewModel
{
    public class UserViewModel
    {
        [Key]
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
