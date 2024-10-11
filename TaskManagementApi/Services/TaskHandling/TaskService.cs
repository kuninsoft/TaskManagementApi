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

    public async Task<TaskDto> GetTask(int id)
    {
        TaskEntity task = (await QueryTasks().ToListAsync()).FirstOrDefault(task => task.Id == id) ??
                          throw new KeyNotFoundException($"Task with ID {id} was not found");

        return task.AsDto();
    }

    public async Task<TaskDto> CreateTask(CreateTaskDto createTaskDto)
    {
        var taskEntity = new TaskEntity
        {
            Title = createTaskDto.Title,
            Description = createTaskDto.Description,
            Priority = createTaskDto.Priority.ToEntityEnum(),
            ProjectId = createTaskDto.ProjectId,
            ReporterUserId = createTaskDto.ReporterUserId,
            AssignedUserId = createTaskDto.AssignedUserId,
            CreatedDate = DateTime.UtcNow
        };
        
        dbContext.Tasks.Add(taskEntity);
        await dbContext.SaveChangesAsync();

        return taskEntity.AsDto();
    }

    public async Task UpdateTask(int id, UpdateTaskDto updateTaskDto)
    {
        TaskEntity task = await dbContext.Tasks.FindAsync(id) 
                          ?? throw new KeyNotFoundException($"Task with ID {id} was not found");
        
        if (!string.IsNullOrWhiteSpace(updateTaskDto.Title)) task.Title = updateTaskDto.Title;
        if (updateTaskDto.Description is not null) task.Description = updateTaskDto.Description;
        if (updateTaskDto.Priority.HasValue) task.Priority = updateTaskDto.Priority.Value.ToEntityEnum();
        if (updateTaskDto.Status.HasValue) task.Status = updateTaskDto.Status.Value.ToEntityEnum();
        if (updateTaskDto.IsFlagged.HasValue) task.IsFlagged = updateTaskDto.IsFlagged.Value;
        if (updateTaskDto.ReporterUserId.HasValue) task.ReporterUserId = updateTaskDto.ReporterUserId.Value;
        if (updateTaskDto.AssignedUserId.HasValue) task.AssignedUserId = updateTaskDto.AssignedUserId.Value;

        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteTask(int id)
    {
        TaskEntity task = await dbContext.Tasks.FindAsync(id)
                          ?? throw new KeyNotFoundException($"Task with ID {id} was not found");
        
        dbContext.Tasks.Remove(task);
        await dbContext.SaveChangesAsync();
    }

    private IQueryable<TaskEntity> QueryTasks()
    {
        return dbContext.Tasks
                        .Include(task => task.Project)
                        .Include(task => task.ReporterUser)
                        .Include(task => task.AssignedUser);
    }
}