using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace TaskManagementApi.Data.Repositories;

public class TaskRepository(AppDbContext dbContext) : ITaskRepository
{
    public Task<List<Entities.Task>> GetAllAsync()
    {
        return dbContext.Tasks
                        .Include(task => task.Project)
                        .Include(task => task.ReporterUser)
                        .Include(task => task.AssignedUser)
                        .ToListAsync();
    }
    
    public Task<Entities.Task?> GetByIdAsync(int id)
    {
        return GetByFilterAsync(task => task.Id == id);
    }
    
    public Task<Entities.Task?> GetByFilterAsync(Expression<Func<Entities.Task, bool>> predicate)
    {
        return dbContext.Tasks
                        .Include(task => task.Project)
                        .Include(task => task.ReporterUser)
                        .Include(task => task.AssignedUser)
                        .FirstOrDefaultAsync(predicate);
    }

    public async Task<Entities.Task> CreateAsync(Entities.Task entity)
    {
        dbContext.Tasks.Add(entity);
        await dbContext.SaveChangesAsync();
        
        await dbContext.Entry(entity)
                       .Reference(task => task.Project)
                       .LoadAsync();
        
        await dbContext.Entry(entity)
                       .Reference(task => task.ReporterUser)
                       .LoadAsync();
        
        return entity;
    }

    public async Task<Entities.Task> UpdateAsync(Entities.Task entity)
    {
        dbContext.Tasks.Update(entity);
        await dbContext.SaveChangesAsync();
        
        return entity;
    }

    public async Task DeleteAsync(Entities.Task entity)
    {
        dbContext.Tasks.Remove(entity);
        await dbContext.SaveChangesAsync();
    }
}