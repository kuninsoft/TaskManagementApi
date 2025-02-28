using TaskManagementApi.Data.Repositories;
using TaskManagementApi.Extensions;
using TaskManagementApi.Models.Tasks;
using TaskManagementApi.Services.TimeProvider;
using TaskEntity = TaskManagementApi.Data.Entities.Task;

namespace TaskManagementApi.Services.TaskHandling;

public class TaskService(ITaskRepository repository, ITimeProvider timeProvider) : ITaskService
{
    public async Task<List<TaskDto>> GetAllTasks()
    {
        List<TaskEntity> tasks = await repository.GetAllAsync();

        return tasks.Select(task => task.AsDto()).ToList();
    }

    public async Task<TaskDto> GetTask(int id)
    {
        TaskEntity task = await repository.GetByIdAsync(id) ??
                          throw new KeyNotFoundException($"Task with ID {id} was not found");

        return task.AsDto();
    }

    public async Task<TaskDto> CreateTask(CreateTaskDto createTaskDto)
    {
        if (createTaskDto.DueDate.HasValue && createTaskDto.DueDate.Value < timeProvider.Now)
        {
            throw new ArgumentException("Due date must not be in the past");
        } 
        
        var taskEntity = new TaskEntity
        {
            Title = createTaskDto.Title,
            Description = createTaskDto.Description,
            Priority = createTaskDto.Priority.ToEntityEnum(),
            ProjectId = createTaskDto.ProjectId,
            ReporterUserId = createTaskDto.ReporterUserId,
            AssignedUserId = createTaskDto.AssignedUserId,
            CreatedDate = timeProvider.UtcNow
        };
        
        taskEntity = await repository.CreateAsync(taskEntity);
        
        return taskEntity.AsDto();
    }

    public async Task UpdateTask(int id, UpdateTaskDto updateTaskDto)
    {
        TaskEntity task = await repository.GetByIdAsync(id)
                          ?? throw new KeyNotFoundException($"Task with ID {id} was not found");

        if (updateTaskDto.Title is not null)
        {
            if (string.IsNullOrWhiteSpace(updateTaskDto.Title))
            {
                throw new ArgumentException("Title cannot be empty if passed");
            }
            
            task.Title = updateTaskDto.Title;
        }

        if (updateTaskDto.Description is not null)
        {
            task.Description = updateTaskDto.Description;
        }

        if (updateTaskDto.Priority.HasValue)
        {
            task.Priority = updateTaskDto.Priority.Value.ToEntityEnum();
        }
        
        if (updateTaskDto.Status.HasValue)
        {
            task.Status = updateTaskDto.Status.Value.ToEntityEnum();
        }
        
        if (updateTaskDto.IsFlagged.HasValue)
        {
            task.IsFlagged = updateTaskDto.IsFlagged.Value;
        }
        
        if (updateTaskDto.ReporterUserId.HasValue)
        {
            task.ReporterUserId = updateTaskDto.ReporterUserId.Value;
        }
        
        if (updateTaskDto.AssignedUserId.HasValue)
        {
            task.AssignedUserId = updateTaskDto.AssignedUserId.Value;
        }
        
        if (updateTaskDto.DueDate.HasValue)
        {
            if (updateTaskDto.DueDate.Value < task.CreatedDate)
            {
                throw new ArgumentException("Due date cannot be set before task creation");
            }
            
            task.DueDate = updateTaskDto.DueDate.Value;
        }

        await repository.UpdateAsync(task);
    }

    public async Task DeleteTask(int id)
    {
        TaskEntity task = await repository.GetByIdAsync(id)
                          ?? throw new KeyNotFoundException($"Task with ID {id} was not found");
        
        await repository.DeleteAsync(task);
    }
}