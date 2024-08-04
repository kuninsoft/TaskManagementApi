using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Data;
using TaskManagementApi.Data.Entities;
using TaskManagementApi.Data.Entities.Enums;
using TaskManagementApi.Extensions;
using TaskManagementApi.Models;

using Task = System.Threading.Tasks.Task;

namespace TaskManagementApi.Services.UserHandling;

// TODO Make repository/unit of work
public class UserService(AppDbContext dbContext) : IUserService
{
    public Task<List<UserDto>> GetAllUsers()
    {
        return Task.FromResult(dbContext.Users.Select(user => user.ToUserDto()).ToList());
    }
    
    public async Task<UserDto> GetUser(int id)
    {
        User user = await dbContext.Users.FindAsync(id)
                    ?? throw new KeyNotFoundException();

        return user.ToUserDto();
    }

    public async Task<UserDto> CreateUser(CreateUserDto createUserDto)
    {
        var userEntity = new User
        {
            Username = createUserDto.Username,
            Email = createUserDto.Email,
            FullName = createUserDto.FullName,
            PasswordHash = createUserDto.Password, // TODO: Hash
            CreatedDate = DateTime.Now,
            Role = Role.User
        };

        await dbContext.Users.AddAsync(userEntity);
        await dbContext.SaveChangesAsync();

        return userEntity.ToUserDto();
    }

    public async Task UpdateUser(int id, UpdateUserDto updateUserDto)
    {
        User user = await dbContext.Users.FindAsync(id) 
                    ?? throw new KeyNotFoundException();

        if (updateUserDto.FullName != null) user.FullName = updateUserDto.FullName;
        if (updateUserDto.Username != null) user.Username = updateUserDto.Username;
        if (updateUserDto.Email != null) user.Email = updateUserDto.Email;

        dbContext.Users.Update(user);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteUser(int id)
    {
        User user = await dbContext.Users.FindAsync(id) 
                    ?? throw new KeyNotFoundException();

        dbContext.Users.Remove(user);
        await dbContext.SaveChangesAsync();
    }

    public async Task AssignProject(AssignProjectDto assignProjectDto)
    {
        User user = await dbContext.Users
                                   .Include(u => u.AssignedProjects)
                                   .FirstOrDefaultAsync(u => u.Id == assignProjectDto.UserId)
                    ?? throw new KeyNotFoundException("User was not found");

        Project project = await dbContext.Projects
                                         .Include(p => p.AssignedUsers)
                                         .FirstOrDefaultAsync(p => p.Id == assignProjectDto.ProjectId)
                          ?? throw new KeyNotFoundException("Project was not found");
        
        user.AssignedProjects.Add(project);
        project.AssignedUsers.Add(user);

        dbContext.Users.Update(user);
        dbContext.Projects.Update(project);

        await dbContext.SaveChangesAsync();
    }
}