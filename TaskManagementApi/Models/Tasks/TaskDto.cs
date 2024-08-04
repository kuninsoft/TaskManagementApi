using System.ComponentModel.DataAnnotations;
using TaskManagementApi.Models.Enums;
using TaskStatus = TaskManagementApi.Models.Enums.TaskStatus;

namespace TaskManagementApi.Models;

public class TaskDto
{
    public int Id { get; set; }
    
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    public TaskStatus Status { get; set; }
    public TaskPriority Priority { get; set; }
    
    public DateTime DueDate { get; set; }
    
    public bool IsFlagged { get; set; }
    
    public ProjectSummaryDto Project { get; set; }

    public UserSummaryDto AssignedUser { get; set; } = null!;
    public UserSummaryDto ReporterUser { get; set; } = null!;
}