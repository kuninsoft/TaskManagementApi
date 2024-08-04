using System.ComponentModel.DataAnnotations;

namespace TaskManagementApi.Models;

public class CreateProjectDto
{
    [Required] [StringLength(255)] public string Title { get; set; } = null!;

    public string? Description { get; set; }

    [Required] [Range(1, int.MaxValue)] public int OwnerId { get; set; }
}