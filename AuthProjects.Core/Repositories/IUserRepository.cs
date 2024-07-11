using AuthProjects.Core.Domains;

namespace AuthProjects.Core.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetUserByEmail(string email);
    }
}