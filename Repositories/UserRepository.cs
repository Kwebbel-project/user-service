using user_service.Data;
using user_service.Models;

namespace user_service.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApiDbContext _context;

        public UserRepository(ApiDbContext context) 
        {
            _context = context;
        }

        public void CreateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _context.Users.Add(user);
        }

        public void DeleteUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _context.Users.Remove(user);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUserById(long id)
        {
            return _context.Users.FirstOrDefault(user => user.Id == id);
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(user => user.Email == email);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
