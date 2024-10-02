using TaskManagementApi.Data.Entities;
using TaskManagementApi.Models.Enums;
using TaskManagementApi.Models.Project;

using EntityEnums = TaskManagementApi.Data.Entities.Enums;

namespace TaskManagementApi.Extensions;

public static class ProjectEntityExtensions
{
    public static ProjectDto ToProjectDto(this Project project)
    {
        return new ProjectDto
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            CreatedDate = project.CreatedDate,
            DueDate = project.DueDate,
            Status = ConvertProjectStatus(project.Status),
            Owner = project.Owner.ToUserSummaryDto(),
            ProjectTeam = project.AssignedUsers.Select(user => user.ToUserSummaryDto()).ToList()
        };
    }

    private static ProjectStatus ConvertProjectStatus(TaskManagementApi.Data.Entities.Enums.ProjectStatus status)
    {
        return status switch
        {
            EntityEnums.ProjectStatus.Active => ProjectStatus.Active,
            EntityEnums.ProjectStatus.Completed => ProjectStatus.Completed,
            EntityEnums.ProjectStatus.OnHold => ProjectStatus.OnHold,
            EntityEnums.ProjectStatus.Canceled => ProjectStatus.Canceled,
            _ => throw new ArgumentOutOfRangeException(nameof(status), "Invalid status passed")
        };
    }

    public static ProjectSummaryDto ToProjectSummaryDto(this Project project)
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
}