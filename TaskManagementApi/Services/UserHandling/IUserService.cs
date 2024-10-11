using TaskManagementApi.Models;
using TaskManagementApi.Models.User;

namespace TaskManagementApi.Services.UserHandling;

public interface IUserService
{
    Task<List<UserDto>> GetAllUsers();
    Task<UserDto> GetUser(int id);
    Task<UserDto> CreateUser(CreateUserDto createUserDto);
    Task UpdateUser(int id, UpdateUserDto updateUserDto);
    Task DeleteUser(int id);
    Task AssignProject(AssignProjectDto assignProjectDto);
}