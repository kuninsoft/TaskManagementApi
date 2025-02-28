using TaskManagementApi.Models.Enums;
using TaskManagementApi.Models.Project;
using TaskManagementApi.Models.User;
using TaskStatus = TaskManagementApi.Models.Enums.TaskStatus;

namespace TaskManagementApi.Models.Tasks;

public class TaskDto
{
    public int Id { get; init; }
    
    public string Title { get; init; } = null!;
    public string Description { get; init; } = null!;
    
    public TaskStatus Status { get; init; }
    public TaskPriority Priority { get; init; }
    
    public DateTime CreatedDate { get; init; }
    public DateTime? DueDate { get; init; }
    
    public bool IsFlagged { get; init; }

    public ProjectSummaryDto Project { get; init; } = null!;
    
    public UserSummaryDto? ReporterUser { get; init; }
    public UserSummaryDto? AssignedUser { get; init; }
}