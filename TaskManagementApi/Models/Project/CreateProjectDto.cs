using System.ComponentModel.DataAnnotations;
using TimeProvider = TaskManagementApi.Services.TimeProvider;

namespace TaskManagementApi.Models.Project;

public class CreateProjectDto
{
    [Required] [StringLength(255)] [MinLength(1)] public string Title { get; init; } = null!;

    public string Description { get; init; } = string.Empty;

    public DateTime? DueDate { get; init; } 

    [Required] [Range(1, int.MaxValue)] public int OwnerId { get; init; }
}