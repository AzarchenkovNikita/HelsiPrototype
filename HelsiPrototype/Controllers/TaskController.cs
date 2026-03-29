using HelsiPrototype.DTO;
using HelsiPrototype.Interfaces;
using HelsiPrototype.Model;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("task")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _service;

    public TaskController(ITaskService service)
    {
        _service = service;
    }

    [HttpPost("getrange")]
    public async Task<IActionResult> GetRange([FromBody] TaskGet_Range dto)
    {
        List<TaskEntity> taskRange = await _service.GetRangeAsync(dto);
        return Ok(new { taskRange });
    }

    [HttpGet("get/{id}")]
    public async Task<IActionResult> Get(string id)
    {
        TaskEntity task = await _service.GetAsync(id);
        return Ok(new { task });
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] Task_Add dto)
    {
        string taskId = await _service.CreateAsync(dto);
        return Ok(new { taskId });
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update([FromBody] Task_Upd dto)
    {
        await _service.UpdateAsync(dto);
        return Ok(new {
            result = "success"
        });
    }

    [HttpPost("delete/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _service.DeleteAsync(id);
        return Ok(new {
            result = "success"
        });
    }
}