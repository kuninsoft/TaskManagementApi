using System.ComponentModel.DataAnnotations;
using TaskManagementApi.Models.Enums;

namespace TaskManagementApi.Models.Project;

public class UpdateProjectDto
{
    [StringLength(255)] public string? Name { get; init; }
    public string? Description { get; init; }
    public DateTime? DueDate { get; init; }
    public ProjectStatus? ProjectStatus { get; init; }
}