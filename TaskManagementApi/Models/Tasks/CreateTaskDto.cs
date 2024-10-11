using System.ComponentModel.DataAnnotations;
using TaskManagementApi.Models.Enums;

namespace TaskManagementApi.Models.Tasks;

public class CreateTaskDto
{
    [Required] public string Title { get; set; } = null!;
    
    [Required] public string Description { get; set; } = null!;
    
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;
    
    [Required] public int ProjectId { get; set; }
    
    [Required] public int ReporterUserId { get; set; }
    
    public int? AssignedUserId { get; set; }
}