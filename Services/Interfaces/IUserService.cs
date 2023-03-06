using Microsoft.AspNetCore.Mvc;
using user_service.Models;

namespace user_service.Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
        void DeleteUser(User user);
    }
}
