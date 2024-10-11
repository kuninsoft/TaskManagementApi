using TaskManagementApi.Models.Tasks;

namespace TaskManagementApi.Services.TaskHandling;

public interface ITaskService
{
    Task<List<TaskDto>> GetAllTasks();
    Task<TaskDto> GetTask(int id);
    Task<TaskDto> CreateTask(CreateTaskDto createProjectDto);
    Task UpdateTask(int id, UpdateTaskDto updateProjectDto);
    Task DeleteTask(int id);
}