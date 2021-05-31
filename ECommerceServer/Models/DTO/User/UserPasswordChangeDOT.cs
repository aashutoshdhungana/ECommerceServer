using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ECommerceServer.Models.DTO.User
{
    public class UserPasswordChangeDOT
    {
        [Required]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        [RegularExpression(@"(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^\w]).{8,25}",
            ErrorMessage = "Password must contain both upper and lower case with atleast " +
            "one digit and special character, length in between 8-25")]
        public string OldPassword { get; set; }

        [Required]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        [RegularExpression(@"(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^\w]).{8,25}",
            ErrorMessage = "Password must contain both upper and lower case with atleast " +
            "one digit and special character, length in between 8-25")]
        public string NewPassword { get; set; }
    }
}
