using System.ComponentModel.DataAnnotations;
using TaskManagementApi.Models.Enums;
using TaskStatus = TaskManagementApi.Models.Enums.TaskStatus;

namespace TaskManagementApi.Models;

public class Task
{
    public Guid Id { get; set; }
    
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    public TaskStatus Status { get; set; }
    public TaskPriority Priority { get; set; }
    
    public DateTime DueDate { get; set; }
    
    public bool IsFlagged { get; set; }
    //
    // public Project? Project { get; set; }
    //
    // public User? AssignedUser { get; set; }
    // public User? ReporterUser { get; set; }
    //
    // TODO: Add Comments & Attachments
}