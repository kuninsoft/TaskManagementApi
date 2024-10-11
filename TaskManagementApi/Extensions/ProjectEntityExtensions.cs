using TaskManagementApi.Data.Entities;
using TaskManagementApi.Models.Enums;
using TaskManagementApi.Models.Project;

using EntityEnums = TaskManagementApi.Data.Entities.Enums;

namespace TaskManagementApi.Extensions;

public static class ProjectEntityExtensions
{
    public static ProjectDto AsDto(this Project project)
    {
        return new ProjectDto
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            CreatedDate = project.CreatedDate,
            DueDate = project.DueDate,
            Status = ConvertProjectStatus(project.Status),
            Owner = project.Owner.AsSummaryDto(),
            ProjectTeam = project.AssignedUsers.Select(user => user.AsSummaryDto()).ToList(),
            Tasks = project.Tasks.Select(task => task.AsSummaryDto()).ToList()
        };
    }
    
    public static ProjectSummaryDto AsSummaryDto(this Project project)
    {
        return new ProjectSummaryDto
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            CreatedDate = project.CreatedDate,
            DueDate = project.DueDate,
            Status = ConvertProjectStatus(project.Status)
        };
    }

    private static ProjectStatus ConvertProjectStatus(EntityEnums.ProjectStatus status)
    {
        return status switch
        {
            EntityEnums.ProjectStatus.Active => ProjectStatus.Active,
            EntityEnums.ProjectStatus.Completed => ProjectStatus.Completed,
            EntityEnums.ProjectStatus.OnHold => ProjectStatus.OnHold,
            EntityEnums.ProjectStatus.Canceled => ProjectStatus.Canceled,
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, "Invalid status passed")
        };
    }
}