using AuthProjects.Core.Domains;
using AuthProjects.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AuthProjects.Infrastructures.Repositories.Users
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(CoreDbContext context) : base(context)
        {
        }

        public Task<User?> GetUserByEmail(string email)
        {
            return ReadAsync(delegate (DbSet<User> dbSet)
            {
                return dbSet.Where(item => item.Email == email);
            });
        }

    }
}