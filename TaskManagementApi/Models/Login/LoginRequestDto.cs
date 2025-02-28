using Microsoft.Build.Framework;

namespace TaskManagementApi.Models.Login;

public class LoginRequestDto
{
    [Required] public string Username { get; init; } = null!;

    [Required] public string Password { get; init; } = null!;
}