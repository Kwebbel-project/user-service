using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using user_service.Dto;
using user_service.Models;
using user_service.Repositories;

namespace user_service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
        }

        public User CreateUser(User user)
        {
            _repository.CreateUser(user);
            _repository.SaveChanges();

            return user;
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
