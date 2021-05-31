using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ECommerceServer.Models.Enumerations;

namespace ECommerceServer.Models.DTO.User
{
    public class UserUpdateDTO
    {
        [Required]
        [MaxLength(30)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(320)]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Date of Birth")]
        public DateTime DOB { get; set; }

        [Required]
        [DisplayName("Role")]
        public Role Role { get; set; }
    }
}
