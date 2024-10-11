using TaskManagementApi.Models.Enums;
using TaskManagementApi.Models.Project;
using TaskManagementApi.Models.User;
using TaskStatus = TaskManagementApi.Models.Enums.TaskStatus;

namespace TaskManagementApi.Models.Tasks;

public class TaskDto
{
    public int Id { get; set; }
    
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    
    public TaskStatus Status { get; set; }
    public TaskPriority Priority { get; set; }
    
    public DateTime CreatedDate { get; set; }
    public DateTime? DueDate { get; set; }
    
    public bool IsFlagged { get; set; }

    public ProjectSummaryDto Project { get; set; } = null!;
    
    public UserSummaryDto ReporterUser { get; set; } = null!;
    public UserSummaryDto? AssignedUser { get; set; }
}