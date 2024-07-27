using System.ComponentModel.DataAnnotations;
using TaskManagementApi.Data.Entities.Enums;

namespace TaskManagementApi.Data.Entities;

public class User
{
    [Key]
    public int Id { get; set; }

    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string FullName { get; set; } = null!;
    
    public string PasswordHash { get; set; } = null!;
    
    public Role Role { get; set; }
    
    public DateTime CreatedDate { get; set; }

    public List<Project> AssignedProjects { get; set; } = [];
    public List<Project> OwnedProjects { get; set; } = [];
    public List<Task> AssignedTasks { get; set; } = [];
    public List<Task> ReportedTasks { get; set; } = [];
    
}