using user_service.Models;

namespace user_service.Repositories
{
    public interface IUserRepository
    {
        bool SaveChanges();
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
        void CreateUser(User user);
        void DeleteUser(User user);
    }
}
