using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using user_service.Models;
using user_service.Repositories;
using user_service.Services.Interfaces;

namespace user_service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
        }

        public void DeleteUser(User user)
        {
            _repository.DeleteUser(user);
        }

        public IEnumerable<User> GetAllUsers()
        {
            var users = _repository.GetAllUsers();
            if (users != null)
            {
                return users;
            }
            else
            {
                return null;
            }
        }

        public User GetUserById(int id)
        {
            return _repository.GetUserById(id);
        }
    }
}
