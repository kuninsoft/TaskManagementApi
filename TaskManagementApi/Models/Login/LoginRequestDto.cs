using Microsoft.Build.Framework;

namespace TaskManagementApi.Models.Login;

public class LoginRequestDto
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}