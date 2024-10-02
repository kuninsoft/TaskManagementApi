using System.ComponentModel.DataAnnotations;

namespace TaskManagementApi.Models.Project;

public class CreateProjectDto
{
    [Required] [StringLength(255)] public string Title { get; set; } = null!;

    public string Description { get; set; } = string.Empty;

    public DateTime DueDate { get; set; } = DateTime.Today.AddMonths(1);

    [Required] [Range(1, int.MaxValue)] public int OwnerId { get; set; }
}