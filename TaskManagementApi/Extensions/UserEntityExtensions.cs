using TaskManagementApi.Data.Entities;
using TaskManagementApi.Models;

namespace TaskManagementApi.Extensions;

public static class UserEntityExtensions
{
    public static UserDto ToUserDto(this User entity)
    {
        return new UserDto
        {
            Id = entity.Id,
            Username = entity.Username,
            Email = entity.Email,
            FullName = entity.FullName,
            CreatedDate = entity.CreatedDate,
            OwnedProjects = entity.OwnedProjects.Select(project => project.ToProjectSummaryDto()).ToList(),
            AssignedProjects = entity.AssignedProjects.Select(project => project.ToProjectSummaryDto()).ToList(),
            // AssignedTasks = entity.AssignedTasks.Select(task => task.ToTaskSummaryDto()).ToList()
        };
    }

    public static UserSummaryDto ToUserSummaryDto(this User entity)
    {
        return new UserSummaryDto
        {
            Id = entity.Id,
            Username = entity.Username,
            Email = entity.Email,
            FullName = entity.FullName,
            CreatedDate = entity.CreatedDate
        };
    }
}