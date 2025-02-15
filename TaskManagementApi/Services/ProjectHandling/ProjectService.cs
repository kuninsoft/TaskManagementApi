using TaskManagementApi.Data.Entities;
using TaskManagementApi.Data.Repositories;
using TaskManagementApi.Extensions;
using TaskManagementApi.Models.Project;
using Task = System.Threading.Tasks.Task;

namespace TaskManagementApi.Services.ProjectHandling;

public class ProjectService(IProjectRepository repository) : IProjectService
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
        if (createProjectDto.DueDate < TimeProvider.Now)
        {
            throw new ArgumentException("Due date cannot be in the past");
        }
        
        var projectEntity = new Project
        {
            Name = createProjectDto.Title,
            Description = createProjectDto.Description,
            OwnerId = createProjectDto.OwnerId,
            CreatedDate = TimeProvider.UtcNow,
            DueDate = createProjectDto.DueDate
        };

        await repository.CreateAsync(projectEntity);
        
        return projectEntity.AsDto();
    }

    public async Task UpdateProject(int id, UpdateProjectDto updateProjectDto)
    {
        Project project = await repository.GetByIdAsync(id)
                          ?? throw new KeyNotFoundException($"Project with ID {id} was not found");

        if (updateProjectDto.Name is not null)
        {
            if (string.IsNullOrWhiteSpace(updateProjectDto.Name))
            {
                throw new ArgumentException("Project name cannot be an empty string or whitespace");
            }
            
            project.Name = updateProjectDto.Name;
        }

        if (updateProjectDto.Description is not null)
        {
            project.Description = updateProjectDto.Description;
        }
        
        if (updateProjectDto.DueDate.HasValue)
        {
            if (updateProjectDto.DueDate < project.CreatedDate)
            {
                throw new ArgumentException("Due date cannot be set to a date before the project was created");
            }
            
            project.DueDate = updateProjectDto.DueDate.Value;
        }

        if (updateProjectDto.ProjectStatus.HasValue)
        {
            project.Status = updateProjectDto.ProjectStatus.Value.ToEntityEnum();
        }

        await repository.UpdateAsync(project);
    }

    public async Task DeleteProject(int id)
    {
        Project project = await repository.GetByIdAsync(id) 
                          ?? throw new KeyNotFoundException($"Project with ID {id} was not found");
        
        await repository.DeleteAsync(project);
    }
}