using TaskManagementApi.Models;

namespace TaskManagementApi.Services.UserHandling;

public class MockUserService : IUserService
{
    public async Task<List<UserDto>> GetAllUsers()
    {
        return [];
    }

    public Task<UserDto> GetUser(int id)
    {
        return Task.FromResult(new UserDto
        {
            Id = Random.Shared.Next(),
            CreatedDate = DateTime.Now
        });
    }

    public Task<UserDto> CreateUser(CreateUserDto createUserDto)
    {
        return Task.FromResult(new UserDto
        {
            Id = Random.Shared.Next(),
            Username = createUserDto.Username,
            Email = createUserDto.Email,
            FullName = createUserDto.FullName,
            CreatedDate = DateTime.Now
        });
    }

    public Task UpdateUser(int id, UpdateUserDto updateUserDto)
    {
        return Task.CompletedTask;
    }

    public Task DeleteUser(int id)
    {
        return Task.CompletedTask;
    }

    public Task AssignProject(AssignProjectDto assignProjectDto)
    {
        return Task.CompletedTask;
    }
}