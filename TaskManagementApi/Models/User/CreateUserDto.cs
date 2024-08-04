using System.ComponentModel.DataAnnotations;

namespace TaskManagementApi.Models;

public class CreateUserDto
{
    [Required] [StringLength(100)] public string Username { get; set; } = null!;

    [Required]
    [StringLength(255)]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required] [StringLength(255)] public string FullName { get; set; } = null!;

    [Required]
    [StringLength(255, MinimumLength = 8)]
    public string Password { get; set; } = null!;
}