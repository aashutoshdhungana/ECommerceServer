using System;
namespace ECommerceServer.Services
{
    public interface IJWTAuthentication
    {
        string Authenticate(string email, string userId);
    }
}
