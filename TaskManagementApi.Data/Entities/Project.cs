using System.ComponentModel.DataAnnotations;
using TaskManagementApi.Data.Entities.Enums;

namespace TaskManagementApi.Data.Entities;

public class Project
{
    [Key]
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    public DateTime CreatedDate { get; set; }
    public DateTime DueDate { get; set; }
    
    public ProjectStatus Status { get; set; }
    
    public int? OwnerId { get; set; }
    public User? Owner { get; set; }

    public List<User> AssignedUsers { get; set; } = [];
    public List<Task> Tasks { get; set; } = [];
}