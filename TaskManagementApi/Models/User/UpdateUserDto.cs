namespace TaskManagementApi.Models.User;

public class UpdateUserDto
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? FullName { get; set; }
}