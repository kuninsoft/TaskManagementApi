using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.Models;

namespace TaskManagementApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<TaskDto>> Get()
    {
        return Ok(new [] { new TaskDto(), new TaskDto() });
    }

    [HttpGet("{id}")]
    public ActionResult<TaskDto> Get(int id)
    {
        return Ok(new TaskDto());
    }

    [HttpPost]
    public IActionResult Post([FromBody] TaskDto value)
    {
        return Created("none", value);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] TaskDto value)
    {
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return Ok();
    }
}