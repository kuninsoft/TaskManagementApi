using System.ComponentModel.DataAnnotations;
using TaskManagementApi.Models.Enums;

namespace TaskManagementApi.Models;

public class Project
{
    [Key]
    public Guid Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    public DateTime CreatedDate { get; set; }
    public DateTime DueDate { get; set; }
    
    public ProjectStatus Status { get; set; }
    
    public User? Owner { get; set; }
    public IEnumerable<User>? ProjectTeam { get; set; }
}