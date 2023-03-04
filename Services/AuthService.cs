using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Core.Types;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using user_service.Models;
using user_service.Models.Dto;
using user_service.Repositories;
using user_service.Security;
using user_service.Services.Interfaces;

namespace user_service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly JwtValidator _jwtValidator;

        public AuthService(IConfiguration configuration, IUserRepository repository, JwtValidator jwtValidator)
        {
            this._configuration = configuration;
            _repository = repository;
            _jwtValidator = jwtValidator;
        }

        public string Login(UserLoginDto userLoginDto)
        {
            var userFound = _repository.GetUserByEmail(userLoginDto.Email);
            if (userFound != null)
            {
                if (BCrypt.Net.BCrypt.Verify(userLoginDto.Password, userFound.Password))
                {
                    string token = _jwtValidator.GenerateToken(userFound);
                    return token;
                }
            }
            return null;
        }

        public User Register(UserCreateDto userCreateDto)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(userCreateDto.Password);
            User user = new User();

            user.Email = userCreateDto.Email;
            user.Name = userCreateDto.Name;
            user.Password = passwordHash;

            _repository.CreateUser(user);
            _repository.SaveChanges();

            return user;
        }
    }
}
