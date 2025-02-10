using System.ComponentModel.DataAnnotations;
using TimeProvider = TaskManagementApi.Services.TimeProvider;

namespace TaskManagementApi.Models.Project;

public class CreateProjectDto
{
    [Required] [StringLength(255)] public string Title { get; set; } = null!;

    public string Description { get; set; } = string.Empty;

    public DateTime DueDate { get; set; } = TimeProvider.Now.AddMonths(1);

    [Required] [Range(1, int.MaxValue)] public int OwnerId { get; set; }
}