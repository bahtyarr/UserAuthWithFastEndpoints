using AuthProjects.Infrastructures.Domain;
using Microsoft.EntityFrameworkCore;

namespace AuthProjects.Infrastructures
{
    public class CoreDbContext : DbContext
    {
        public CoreDbContext(DbContextOptions<CoreDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

    }
}