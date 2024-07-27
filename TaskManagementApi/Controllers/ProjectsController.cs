using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.Models;

namespace TaskManagementApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Project>> Get()
    {
        return Ok(new [] { new Project(), new Project() });
    }

    [HttpGet("{id}")]
    public ActionResult<Project> Get(int id)
    {
        return Ok(new Project());
    }

    [HttpPost]
    public IActionResult Post([FromBody] Project value)
    {
        return Created("none", value);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Project value)
    {
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return Ok();
    }
}