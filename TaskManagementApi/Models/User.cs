using System.ComponentModel.DataAnnotations;

namespace TaskManagementApi.Models;

public class User
{
    [Key]
    public Guid Id { get; set; }
    
    public string Username { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastLoginDate { get; set; }
    
    public string PasswordHash { get; set; }
}