namespace TaskManagementApi.Models;

public class UserSummaryDto
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string FullName { get; set; } = null!;
    
    public DateTime CreatedDate { get; set; }
}