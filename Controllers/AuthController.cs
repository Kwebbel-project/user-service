using AutoMapper;
using Confluent.Kafka;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using user_service.Models;
using user_service.Models.Dto;
using user_service.Security;
using user_service.Services.Interfaces;

namespace user_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AuthController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserCreateDto userDto)
        {
            User user = await _authService.Register(userDto);
            return Ok(_mapper.Map<UserReadDto>(user));
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<string> Login(UserLoginDto userDto)
        {
            string token = _authService.Login(userDto);
            if (token != null && token.Length >= 1)
            {
                Response.Cookies.Append("jwtToken", token, new CookieOptions { HttpOnly = true });

                return Ok(token);
            } 
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet("test")]
        [Authorize]
        public ActionResult<string> Test()
        {
            return Ok("authorized");
        }
    }
}
