using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.Models;

namespace TaskManagementApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<ProjectDto>> Get()
    {
        return Ok(new [] { new ProjectDto(), new ProjectDto() });
    }

    [HttpGet("{id}")]
    public ActionResult<ProjectDto> Get(int id)
    {
        return Ok(new ProjectDto());
    }

    [HttpPost]
    public IActionResult Post([FromBody] ProjectDto value)
    {
        return Created("none", value);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] ProjectDto value)
    {
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return Ok();
    }
}