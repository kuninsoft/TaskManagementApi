using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Data;
using TaskManagementApi.Data.Entities;
using TaskManagementApi.Extensions;
using TaskManagementApi.Models.Project;
using Task = System.Threading.Tasks.Task;

namespace TaskManagementApi.Services.ProjectHandling;

public class ProjectService(AppDbContext dbContext) : IProjectService
{
    public async Task<List<ProjectDto>> GetAllProjects()
    {
        List<Project> projectEntities = await QueryProjects().ToListAsync();

        return projectEntities.Select(project => project.AsDto()).ToList();
    }

    public async Task<ProjectDto> GetProject(int id)
    {
        Project project = (await QueryProjects().ToListAsync()).FirstOrDefault(project => project.Id == id)
                                ?? throw new KeyNotFoundException();

        return project.AsDto();
    }

    public async Task<ProjectDto> CreateProject(CreateProjectDto createProjectDto)
    {
        var projectEntity = new Project
        {
            Name = createProjectDto.Title,
            Description = createProjectDto.Description,
            OwnerId = createProjectDto.OwnerId,
            CreatedDate = DateTime.UtcNow,
            DueDate = createProjectDto.DueDate
        };

        await dbContext.Projects.AddAsync(projectEntity);
        await dbContext.SaveChangesAsync();
        
        return projectEntity.AsDto();
    }

    public async Task UpdateProject(int id, UpdateProjectDto updateProjectDto)
    {
        Project project = await dbContext.Projects.FindAsync(id)
                          ?? throw new KeyNotFoundException();
        
        if (updateProjectDto.Name is not null) project.Name = updateProjectDto.Name;
        if (updateProjectDto.Description is not null) project.Description = updateProjectDto.Description;
        if (updateProjectDto.DueDate.HasValue) project.DueDate = updateProjectDto.DueDate.Value;
        if (updateProjectDto.ProjectStatus.HasValue) project.Status = updateProjectDto.ProjectStatus.Value;

        dbContext.Projects.Update(project);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteProject(int id)
    {
        Project project = await dbContext.Projects.FindAsync(id)
                          ?? throw new KeyNotFoundException();
        
        dbContext.Projects.Remove(project);
        await dbContext.SaveChangesAsync();
    }

    private IQueryable<Project> QueryProjects()
    {
        return dbContext.Projects
                 .Include(project => project.AssignedUsers)
                 .Include(project => project.Owner);
    }
}