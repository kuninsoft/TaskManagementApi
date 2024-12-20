using TaskManagementApi.Models.Enums;
using TaskManagementApi.Models.Tasks;
using TaskManagementApi.Models.User;

namespace TaskManagementApi.Models.Project;

public class ProjectDto
{
    public int Id { get; set; }
    
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    
    public DateTime CreatedDate { get; set; }
    public DateTime DueDate { get; set; }
    
    public ProjectStatus Status { get; set; }

    public UserSummaryDto? Owner { get; set; }
    public List<UserSummaryDto> ProjectTeam { get; set; } = [];
    public List<TaskSummaryDto> Tasks { get; set; } = [];
}