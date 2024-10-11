using TaskManagementApi.Models.Tasks;

using Entities = TaskManagementApi.Data.Entities;
using DtoEnums = TaskManagementApi.Models.Enums;
using EntityEnums = TaskManagementApi.Data.Entities.Enums;

namespace TaskManagementApi.Extensions;

public static class TaskEntityExtensions
{
    public static TaskDto AsDto(this Entities.Task task)
    {
        return new TaskDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Status = ConvertTaskStatus(task.Status),
            Priority = ConvertTaskPriority(task.Priority),
            CreatedDate = task.CreatedDate,
            DueDate = task.DueDate,
            IsFlagged = task.IsFlagged,
            Project = task.Project.AsSummaryDto(),
            ReporterUser = task.ReporterUser.AsSummaryDto(),
            AssignedUser = task.AssignedUser?.AsSummaryDto()
        };
    }

    public static TaskSummaryDto AsSummaryDto(this Entities.Task task)
    {
        return new TaskSummaryDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Status = ConvertTaskStatus(task.Status),
            Priority = ConvertTaskPriority(task.Priority),
            CreatedDate = task.CreatedDate,
            DueDate = task.DueDate,
            IsFlagged = task.IsFlagged
        };
    }

    private static DtoEnums.TaskStatus ConvertTaskStatus(EntityEnums.TaskStatus status)
    {
        return status switch
        {
            EntityEnums.TaskStatus.ToDo => DtoEnums.TaskStatus.ToDo,
            EntityEnums.TaskStatus.InProgress => DtoEnums.TaskStatus.InProgress,
            EntityEnums.TaskStatus.InReview => DtoEnums.TaskStatus.InReview,
            EntityEnums.TaskStatus.Done => DtoEnums.TaskStatus.Done,
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, "Invalid status passed")
        };
    }

    private static DtoEnums.TaskPriority ConvertTaskPriority(EntityEnums.TaskPriority priority)
    {
        return priority switch
        {
            EntityEnums.TaskPriority.Low => DtoEnums.TaskPriority.Low,
            EntityEnums.TaskPriority.Medium => DtoEnums.TaskPriority.Medium,
            EntityEnums.TaskPriority.High => DtoEnums.TaskPriority.High,
            _ => throw new ArgumentOutOfRangeException(nameof(priority), priority, "Invalid priority passed")
        };
    }
}