using System.ComponentModel.DataAnnotations;
using TaskManagementApi.Models.Enums;

namespace TaskManagementApi.Models.Tasks;

public class CreateTaskDto
{
    [Required] [MinLength(1)] public string Title { get; set; } = null!;

    [Required] public string Description { get; set; } = string.Empty;
    
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;
    
    [Required] [Range(1, int.MaxValue)] public int ProjectId { get; set; }
    
    [Required] [Range(1, int.MaxValue)] public int ReporterUserId { get; set; }
    
    public int? AssignedUserId { get; set; }
    
    public DateTime? DueDate { get; set; }
}