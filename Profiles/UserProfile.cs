using AutoMapper;
using user_service.Models;
using user_service.Models.Dto;

namespace user_service.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        { 
            CreateMap<User, UserReadDto>();
            CreateMap<UserCreateDto, User>();
        }
    }
}
