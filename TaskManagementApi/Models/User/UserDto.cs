using System.ComponentModel.DataAnnotations;

namespace TaskManagementApi.Models;

public class UserDto
{
    public int Id { get; set; }
    
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public DateTime CreatedDate { get; set; }

    public List<ProjectSummaryDto> OwnedProjects { get; set; } = [];
    public List<ProjectSummaryDto> AssignedProjects { get; set; } = [];
    public List<TaskSummaryDto> AssignedTasks { get; set; } = [];
}