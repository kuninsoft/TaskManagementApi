using TaskManagementApi.Models.Enums;

namespace TaskManagementApi.Models.Project;

public class ProjectSummaryDto
{
    public int Id { get; set; }
    
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    
    public DateTime CreatedDate { get; set; }
    public DateTime? DueDate { get; set; }
    
    public ProjectStatus Status { get; set; }
}