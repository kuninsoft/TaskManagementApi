using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Data.Entities;
using Task = System.Threading.Tasks.Task;

namespace TaskManagementApi.Data.Repositories;

public class UserRepository(AppDbContext dbContext) : IRepository<User>
{
    public Task<List<User>> GetAllAsync()
    {
        return dbContext.Users
                        .Include(user => user.AssignedTasks)
                        .Include(user => user.OwnedProjects)
                        .Include(user => user.AssignedProjects)
                        .ToListAsync();
    }

    public Task<User?> GetByIdAsync(int id)
    {
        return GetByFilterAsync(user => user.Id == id);
    }

    public Task<User?> GetByFilterAsync(Expression<Func<User, bool>> predicate)
    {
        return dbContext.Users
                        .Include(user => user.AssignedTasks)
                        .Include(user => user.OwnedProjects)
                        .Include(user => user.AssignedProjects)
                        .FirstOrDefaultAsync(predicate);
    }

    public async Task<User> CreateAsync(User entity)
    {
        dbContext.Users.Add(entity);
        
        await dbContext.SaveChangesAsync();
        
        return entity;
    }

    public async Task<User> UpdateAsync(User entity)
    {
        dbContext.Users.Update(entity);
        
        await dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task DeleteAsync(User entity)
    {
        dbContext.Users.Remove(entity);
        
        await dbContext.SaveChangesAsync();
    }
}