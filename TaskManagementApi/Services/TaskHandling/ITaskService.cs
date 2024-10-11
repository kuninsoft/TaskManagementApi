using TaskManagementApi.Models.Tasks;

namespace TaskManagementApi.Services.TaskHandling;

public interface ITaskService
{
    Task<List<TaskDto>> GetAllTasks();
    Task<TaskDto> GetTask(int id);
    Task<TaskDto> CreateTask(CreateTaskDto createTaskDto);
    Task UpdateTask(int id, UpdateTaskDto updateTaskDto);
    Task DeleteTask(int id);
}