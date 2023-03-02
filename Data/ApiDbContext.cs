using Microsoft.EntityFrameworkCore;
using user_service.Models;

namespace user_service.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options) { }
        public DbSet<User> Users { get; set; }
    }
}
