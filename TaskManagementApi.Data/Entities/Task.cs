using TaskManagementApi.Data.Entities.Enums;
using TaskStatus = TaskManagementApi.Data.Entities.Enums.TaskStatus;

namespace TaskManagementApi.Data.Entities;

public class Task
{
    public int Id { get; set; }
    
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    public TaskStatus Status { get; set; }
    public TaskPriority Priority { get; set; }
    
    public DateTime CreatedDate { get; set; }
    public DateTime? DueDate { get; set; }
    
    public bool IsFlagged { get; set; }
    
    public int ProjectId { get; set; }
    public Project Project { get; set; } = null!;
    
    public int ReporterUserId { get; set; } 
    public User ReporterUser { get; set; } = null!;
    
    public int? AssignedUserId { get; set; }
    public User? AssignedUser { get; set; }
}