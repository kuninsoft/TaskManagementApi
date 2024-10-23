using TaskManagementApi.Data.Entities;
using TaskManagementApi.Data.Entities.Enums;
using TaskManagementApi.Data.Repositories;
using TaskManagementApi.Extensions;
using TaskManagementApi.Models.User;
using Task = System.Threading.Tasks.Task;

namespace TaskManagementApi.Services.UserHandling;

public class UserService(UserRepository repository, ProjectRepository projectRepository) : IUserService
{
    public async Task<List<UserDto>> GetAllUsers()
    {
        List<User> userEntities = await repository.GetAllAsync();

        return userEntities.Select(user => user.AsDto()).ToList();
    }

    public async Task<UserDto> GetUser(int id)
    {
        User user = await repository.GetByIdAsync(id)
                    ?? throw new KeyNotFoundException($"User with ID {id} was not found");

        return user.AsDto();
    }

    public async Task<UserDto> CreateUser(CreateUserDto createUserDto)
    {
        var userEntity = new User
        {
            Username = createUserDto.Username,
            Email = createUserDto.Email,
            FullName = createUserDto.FullName,
            PasswordHash = createUserDto.Password, // TODO: Hash
            CreatedDate = DateTime.UtcNow,
            Role = Role.User
        };

        await repository.CreateAsync(userEntity);

        return userEntity.AsDto();
    }

    public async Task UpdateUser(int id, UpdateUserDto updateUserDto)
    {
        User user = await repository.GetByIdAsync(id)
                    ?? throw new KeyNotFoundException($"User with ID {id} was not found");

        if (!string.IsNullOrWhiteSpace(updateUserDto.FullName)) user.FullName = updateUserDto.FullName;
        if (!string.IsNullOrWhiteSpace(updateUserDto.Username)) user.Username = updateUserDto.Username;
        if (!string.IsNullOrWhiteSpace(updateUserDto.Email)) user.Email = updateUserDto.Email;

        await repository.UpdateAsync(user);
    }

    public async Task DeleteUser(int id)
    {
        User user = await repository.GetByIdAsync(id)
                    ?? throw new KeyNotFoundException($"User with ID {id} was not found");

        await repository.DeleteAsync(user);
    }

    public async Task AssignProject(AssignProjectDto assignProjectDto)
    {
        User user = await repository.GetByIdAsync(assignProjectDto.UserId)
                    ?? throw new KeyNotFoundException($"User with ID {assignProjectDto.UserId} was not found");

        Project project = await projectRepository.GetByIdAsync(assignProjectDto.ProjectId)
                          ?? throw new KeyNotFoundException(
                              $"Project with ID {assignProjectDto.ProjectId} was not found");

        user.AssignedProjects.Add(project);

        await repository.UpdateAsync(user);
    }
}