using TaskManagementApi.Data.Entities;
using TaskManagementApi.Data.Repositories;
using TaskManagementApi.Extensions;
using TaskManagementApi.Models.Project;
using Task = System.Threading.Tasks.Task;

namespace TaskManagementApi.Services.ProjectHandling;

public class ProjectService(ProjectRepository repository) : IProjectService
{
    public async Task<List<ProjectDto>> GetAllProjects()
    {
        List<Project> projectEntities = await repository.GetAllAsync();

        return projectEntities.Select(project => project.AsDto()).ToList();
    }

    public async Task<ProjectDto> GetProject(int id)
    {
        Project project = await repository.GetByIdAsync(id) 
                          ?? throw new KeyNotFoundException($"Project with ID {id} was not found");

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

        await repository.CreateAsync(projectEntity);
        
        return projectEntity.AsDto();
    }

    public async Task UpdateProject(int id, UpdateProjectDto updateProjectDto)
    {
        Project project = await repository.GetByIdAsync(id)
                          ?? throw new KeyNotFoundException($"Project with ID {id} was not found");
        
        if (!string.IsNullOrWhiteSpace(updateProjectDto.Name)) project.Name = updateProjectDto.Name;
        if (updateProjectDto.Description is not null) project.Description = updateProjectDto.Description;
        if (updateProjectDto.DueDate.HasValue) project.DueDate = updateProjectDto.DueDate.Value;
        if (updateProjectDto.ProjectStatus.HasValue) project.Status = updateProjectDto.ProjectStatus.Value;

        await repository.UpdateAsync(project);
    }

    public async Task DeleteProject(int id)
    {
        Project project = await repository.GetByIdAsync(id) 
                          ?? throw new KeyNotFoundException($"Project with ID {id} was not found");
        
        await repository.DeleteAsync(project);
    }
}