using System.ComponentModel.DataAnnotations;
using TaskManagementApi.Data.Entities.Enums;

namespace TaskManagementApi.Models.Project;

public class UpdateProjectDto
{
    [StringLength(255)] public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public ProjectStatus? ProjectStatus { get; set; }
}