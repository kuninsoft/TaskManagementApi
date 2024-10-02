using TaskManagementApi.Models.Project;

namespace TaskManagementApi.Services.ProjectHandling;

public interface IProjectService
{
    Task<List<ProjectDto>> GetAllProjects();
    Task<ProjectDto> GetProject(int id);
    Task<ProjectDto> CreateProject(CreateProjectDto createProjectDto);
    Task UpdateProject(int id, UpdateProjectDto updateProjectDto);
    Task DeleteProject(int id);
}