using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Data.Entities;
using Task = System.Threading.Tasks.Task;

namespace TaskManagementApi.Data.Repositories;

public class ProjectRepository(AppDbContext dbContext) : IRepository<Project>
{
    public Task<List<Project>> GetAllAsync()
    {
        return dbContext.Projects
                        .Include(project => project.AssignedUsers)
                        .Include(project => project.Owner)
                        .ToListAsync();
    }

    public Task<Project?> GetByIdAsync(int id)
    {
        return GetByFilterAsync(project => project.Id == id);
    }

    public Task<Project?> GetByFilterAsync(Expression<Func<Project, bool>> predicate)
    {
        return dbContext.Projects
                        .Include(project => project.AssignedUsers)
                        .Include(project => project.Owner)
                        .FirstOrDefaultAsync(predicate);
    }

    public async Task<Project> CreateAsync(Project entity)
    {
        dbContext.Projects.Add(entity);
        await dbContext.SaveChangesAsync();
        
        await dbContext.Entry(entity)
                       .Reference(project => project.Owner)
                       .LoadAsync();

        return entity;
    }

    public async Task<Project> UpdateAsync(Project entity)
    {
        dbContext.Projects.Update(entity);
        await dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task DeleteAsync(Project entity)
    {
        dbContext.Projects.Remove(entity);
        await dbContext.SaveChangesAsync();
    }
}