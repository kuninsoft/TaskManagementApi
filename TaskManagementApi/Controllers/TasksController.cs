using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.Models;
using Task = TaskManagementApi.Models.Task;

namespace TaskManagementApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Task>> Get()
    {
        return Ok(new [] { new Task(), new Task() });
    }

    [HttpGet("{id}")]
    public ActionResult<Task> Get(int id)
    {
        return Ok(new Task());
    }

    [HttpPost]
    public IActionResult Post([FromBody] Task value)
    {
        return Created("none", value);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Task value)
    {
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return Ok();
    }
}