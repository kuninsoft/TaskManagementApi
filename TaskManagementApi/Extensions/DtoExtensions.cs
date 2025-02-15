namespace TaskManagementApi.Extensions;

using DtoEnums = Models.Enums;
using EntityEnums = Data.Entities.Enums;

public static class DtoExtensions 
{
    public static EntityEnums.TaskPriority ToEntityEnum(this DtoEnums.TaskPriority priority)
    {
        return priority switch
        {
            DtoEnums.TaskPriority.Low => EntityEnums.TaskPriority.Low ,
            DtoEnums.TaskPriority.Medium => EntityEnums.TaskPriority.Medium,
            DtoEnums.TaskPriority.High => EntityEnums.TaskPriority.High,
            _ => throw new ArgumentOutOfRangeException(nameof(priority), priority, "Invalid priority passed")
        };
    }
    
    public static EntityEnums.TaskStatus ToEntityEnum(this DtoEnums.TaskStatus status)
    {
        return status switch
        {
            DtoEnums.TaskStatus.ToDo => EntityEnums.TaskStatus.ToDo,
            DtoEnums.TaskStatus.InProgress => EntityEnums.TaskStatus.InProgress,
            DtoEnums.TaskStatus.InReview => EntityEnums.TaskStatus.InReview,
            DtoEnums.TaskStatus.Done => EntityEnums.TaskStatus.Done,
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, "Invalid status passed")
        };
    }

    public static EntityEnums.ProjectStatus ToEntityEnum(this DtoEnums.ProjectStatus status)
    {
        return status switch
        {
            DtoEnums.ProjectStatus.Active => EntityEnums.ProjectStatus.Active,
            DtoEnums.ProjectStatus.Completed => EntityEnums.ProjectStatus.Completed,
            DtoEnums.ProjectStatus.Canceled => EntityEnums.ProjectStatus.Canceled,
            DtoEnums.ProjectStatus.OnHold => EntityEnums.ProjectStatus.OnHold,
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, "Invalid status passed")
        };
    }
}