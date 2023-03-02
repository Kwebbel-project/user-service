using Microsoft.AspNetCore.Mvc;
using user_service.Dto;
using user_service.Models;

namespace user_service.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
        User CreateUser(User user);
        void DeleteUser(User user);
    }
}
