using AutoMapper;
using TwnTw_WEB.DTO_Models;
using TwnTw_WEB.Models;

namespace TwnTw_WEB.ModelProfile
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        { 
            CreateMap<User, UserCreateModel>().ReverseMap();
            CreateMap<User, UserUpdateModel>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();
        }
    }
}
