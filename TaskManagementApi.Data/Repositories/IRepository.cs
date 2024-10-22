using System.Linq.Expressions;

namespace TaskManagementApi.Data.Repositories;

public interface IRepository<T> where T : class
{
    Task<List<T>> GetAllAsync();
    
    Task<T?> GetByIdAsync(int id);
    Task<T?> GetByFilterAsync(Expression<Func<T, bool>> predicate);
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}