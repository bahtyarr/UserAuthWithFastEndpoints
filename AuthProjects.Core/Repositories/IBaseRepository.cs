using System.Linq.Expressions;

namespace AuthProjects.Core.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task BulkCreateAsync(List<T> entities);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T?> ReadAsync(Expression<Func<T, bool>> filter);

    }
}