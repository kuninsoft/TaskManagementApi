using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Data;
using TaskManagementApi.Extensions;
using TaskManagementApi.Models.Tasks;

using TaskEntity = TaskManagementApi.Data.Entities.Task;

namespace TaskManagementApi.Services.TaskHandling;

public class TaskService(AppDbContext dbContext) : ITaskService
{
    public async Task<List<TaskDto>> GetAllTasks()
    {
        List<TaskEntity> tasks = await QueryTasks().ToListAsync();

        return tasks.Select(task => task.AsDto()).ToList();
    }

    public Task<TaskDto> GetTask(int id)
    {
        throw new NotImplementedException();
    }

    public Task<TaskDto> CreateTask(CreateTaskDto createProjectDto)
    {
        throw new NotImplementedException();
    }

    public Task UpdateTask(int id, UpdateTaskDto updateProjectDto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteTask(int id)
    {
        throw new NotImplementedException();
    }

    private IQueryable<TaskEntity> QueryTasks()
    {
        return dbContext.Tasks
                        .Include(task => task.Project)
                        .Include(task => task.ReporterUser)
                        .Include(task => task.AssignedUser);
    }
}