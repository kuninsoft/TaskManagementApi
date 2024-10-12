using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.Models.Project;
using TaskManagementApi.Services.ProjectHandling;

namespace TaskManagementApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController(IProjectService projectService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        List<ProjectDto> projects = await projectService.GetAllProjects();

        if (projects.Count == 0)
        {
            return NoContent();
        }
        
        return Ok(projects);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            ProjectDto project = await projectService.GetProject(id);

            return Ok(project);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateProjectDto value)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        ProjectDto createdProject = await projectService.CreateProject(value);
        
        return Created(Url.Action(nameof(Get), new { id = createdProject.Id }), createdProject);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateProjectDto value)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await projectService.UpdateProject(id, value);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await projectService.DeleteProject(id);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }

        return Ok();
    }
}