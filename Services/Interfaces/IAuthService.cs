using NuGet.Common;
using user_service.Models;
using user_service.Models.Dto;

namespace user_service.Services.Interfaces
{
    public interface IAuthService
    {
        Task<User> Register(UserCreateDto userCreateDto);
        string Login(UserLoginDto userLoginDto);
    }
}
