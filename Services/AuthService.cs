using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NuGet.Protocol.Core.Types;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using user_service.Common.Kafka;
using user_service.Models;
using user_service.Models.Dto;
using user_service.Repositories;
using user_service.Security;
using user_service.Services.Interfaces;
using System.Text.Json;

namespace user_service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly JwtValidator _jwtValidator;
        private readonly KafkaProducerHandler _kafkaProducerHandler;

        public AuthService(IConfiguration configuration, IUserRepository repository, JwtValidator jwtValidator, KafkaProducerHandler kafkaProducerHandler)
        {
            this._configuration = configuration;
            _repository = repository;
            _jwtValidator = jwtValidator;
            _kafkaProducerHandler = kafkaProducerHandler;
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

        public async Task<User> Register(UserCreateDto userCreateDto)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(userCreateDto.Password);
            User user = new User();

            user.Email = userCreateDto.Email;
            user.Name = userCreateDto.Name;
            user.Password = passwordHash;

            _repository.CreateUser(user);
            _repository.SaveChanges();

            await _kafkaProducerHandler.sendMessage("USER_REGISTERED", System.Text.Json.JsonSerializer.Serialize(user)); //create enum for topic

            return user;
        }
    }
}
