using ECommerceServer.Models;
using ECommerceServer.Models.DTO.User;
using ECommerceServer.Models.ViewModel;

namespace ECommerceServer.Profile
{
    public class UserProfile : AutoMapper.Profile
    {
        public UserProfile()
        {
            CreateMap<UserCreateDTO, User>();
            CreateMap<UserUpdateDTO, User>();
            CreateMap<User, UserViewModel>();
        }
    }
}
