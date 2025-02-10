using TaskManagementApi.Models.Enums;
using TaskManagementApi.Models.Tasks;
using TaskManagementApi.Models.User;

namespace TaskManagementApi.Models.Project;

public class ProjectDto
{
    public int Id { get; init; }
    
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    
    public DateTime CreatedDate { get; init; }
    public DateTime DueDate { get; init; }
    
    public ProjectStatus Status { get; init; }

    public UserSummaryDto? Owner { get; init; }
    public List<UserSummaryDto> ProjectTeam { get; init; } = [];
    public List<TaskSummaryDto> Tasks { get; init; } = [];
}