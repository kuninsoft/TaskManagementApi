using TaskManagementApi.Data.Entities;
using TaskManagementApi.Models;
using TaskManagementApi.Models.User;

namespace TaskManagementApi.Extensions;

public static class UserEntityExtensions
{
    public static UserDto AsDto(this User entity)
    {
        return new UserDto
        {
            Id = entity.Id,
            Username = entity.Username,
            Email = entity.Email,
            FullName = entity.FullName,
            CreatedDate = entity.CreatedDate,
            OwnedProjects = entity.OwnedProjects.Select(project => project.AsSummaryDto()).ToList(),
            AssignedProjects = entity.AssignedProjects.Select(project => project.AsSummaryDto()).ToList(),
            AssignedTasks = entity.AssignedTasks.Select(task => task.AsSummaryDto()).ToList()
        };
    }

    public static UserSummaryDto AsSummaryDto(this User entity)
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