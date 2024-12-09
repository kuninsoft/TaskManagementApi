using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.Models.Tasks;
using TaskManagementApi.Services.TaskHandling;

namespace TaskManagementApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class TasksController(ITaskService taskService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        List<TaskDto> tasks = await taskService.GetAllTasks();

        if (tasks.Count == 0)
        {
            return NoContent();
        }
        
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            TaskDto task = await taskService.GetTask(id);

            return Ok(task);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateTaskDto value)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        TaskDto createdTask = await taskService.CreateTask(value);

        return Created(Url.Action(nameof(Get), new { id = createdTask.Id }), createdTask);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateTaskDto value)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        try
        {
            await taskService.UpdateTask(id, value);
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
            await taskService.DeleteTask(id);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }

        return Ok();
    }
}