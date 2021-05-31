using System;
using ECommerceServer.Models.Enumerations;

namespace ECommerceServer.Models
{
    public class AuthenticateResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public Role Role { get; set; }
        public double Wallet { get; set; }
        public string JWTToken { get; set; }
    }
}
