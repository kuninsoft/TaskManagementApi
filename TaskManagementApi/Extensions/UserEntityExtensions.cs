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
            CreatedDate = entity.CreatedDate
        };
    }
}