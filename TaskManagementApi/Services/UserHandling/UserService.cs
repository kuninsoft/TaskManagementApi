using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Data;
using TaskManagementApi.Data.Entities;
using TaskManagementApi.Data.Entities.Enums;
using TaskManagementApi.Extensions;
using TaskManagementApi.Models.User;
using Task = System.Threading.Tasks.Task;

namespace TaskManagementApi.Services.UserHandling;

// TODO Make repository/unit of work
public class UserService(AppDbContext dbContext) : IUserService
{
    public async Task<List<UserDto>> GetAllUsers()
    {
        List<User> userEntities = await QueryUsers().ToListAsync();

        return userEntities.Select(user => user.AsDto()).ToList();
    }

    public async Task<UserDto> GetUser(int id)
    {
        User user = (await QueryUsers().ToListAsync()).FirstOrDefault(user => user.Id == id)
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

        dbContext.Users.Add(userEntity);
        await dbContext.SaveChangesAsync();

        return userEntity.AsDto();
    }

    public async Task UpdateUser(int id, UpdateUserDto updateUserDto)
    {
        User user = await dbContext.Users.FindAsync(id)
                    ?? throw new KeyNotFoundException($"User with ID {id} was not found");

        if (!string.IsNullOrWhiteSpace(updateUserDto.FullName)) user.FullName = updateUserDto.FullName;
        if (!string.IsNullOrWhiteSpace(updateUserDto.Username)) user.Username = updateUserDto.Username;
        if (!string.IsNullOrWhiteSpace(updateUserDto.Email)) user.Email = updateUserDto.Email;

        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteUser(int id)
    {
        User user = await dbContext.Users.FindAsync(id)
                    ?? throw new KeyNotFoundException($"User with ID {id} was not found");

        dbContext.Users.Remove(user);
        await dbContext.SaveChangesAsync();
    }

    public async Task AssignProject(AssignProjectDto assignProjectDto)
    {
        User user = await dbContext.Users
                                   .Include(u => u.AssignedProjects)
                                   .FirstOrDefaultAsync(u => u.Id == assignProjectDto.UserId)
                    ?? throw new KeyNotFoundException($"User with ID {assignProjectDto.UserId} was not found");

        Project project = await dbContext.Projects
                                         .FirstOrDefaultAsync(p => p.Id == assignProjectDto.ProjectId)
                          ?? throw new KeyNotFoundException(
                              $"Project with ID {assignProjectDto.ProjectId} was not found");

        user.AssignedProjects.Add(project);

        await dbContext.SaveChangesAsync();
    }

    private IQueryable<User> QueryUsers()
    {
        return dbContext.Users
                        .Include(user => user.AssignedTasks)
                        .Include(user => user.OwnedProjects)
                        .Include(user => user.AssignedProjects);
    }
}