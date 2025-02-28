using TaskManagementApi.Models.Enums;
using TaskStatus = TaskManagementApi.Models.Enums.TaskStatus;

namespace TaskManagementApi.Models.Tasks;

public class UpdateTaskDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool? IsFlagged { get; set; }
    public TaskStatus? Status { get; set; }
    public TaskPriority? Priority { get; set; }
    public int? ReporterUserId { get; set; }
    public int? AssignedUserId { get; set; }
    public DateTime? DueDate { get; set; }
}