using HelsiPrototype.DTO;
using HelsiPrototype.Interfaces;
using HelsiPrototype.Model;
using Microsoft.AspNetCore.Mvc;

namespace HelsiPrototype.Controllers;

[ApiController]
[Route("tasklist")]
public class TaskListController : ControllerBase
{
    private readonly ITaskListService _service;

    public TaskListController(ITaskListService service)
    {
        _service = service;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] TaskListAddObject dto)
    {
        string taskId = await _service.CreateAsync(dto);
        return Ok(new { taskId });
    }

    [HttpPost("get")]
    public async Task<IActionResult> Get([FromBody] TaskListObject dto)
    {
        TaskListResponse response = await _service.GetAsync(dto);
        return Ok(new { response });
    }

    [HttpPost("getrange")]
    public async Task<IActionResult> GetRange([FromBody] TaskListGetRangeObject dto)
    {
        List<TaskList> taskListRange = await _service.GetRangeAsync(dto);
        return Ok(new { taskListRange });
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update([FromBody] TaskListUpdObject dto)
    {
        await _service.UpdateAsync(dto);
        return Ok(new { 
            result = "success"
        });
    }

    [HttpPost("assigntask")]
    public async Task<IActionResult> AssignTask([FromBody] TaskListLinkObject dto)
    {
        await _service.AssignTask(dto);
        return Ok(new {
            result = "success"
        });
    }

    [HttpPost("unassigntask")]
    public async Task<IActionResult> UnassignTask([FromBody] TaskListLinkObject dto)
    {
        await _service.UnassignTask(dto);
        return Ok(new {
            result = "success"
        });
    }

    [HttpPost("assignuser")]
    public async Task<IActionResult> AssignUser([FromBody] TaskListLinkObject dto)
    {
        await _service.AssignUser(dto);
        return Ok(new {
            result = "success"
        });
    }

    [HttpPost("unassignuser")]
    public async Task<IActionResult> UnassignUser([FromBody] TaskListLinkObject dto)
    {
        await _service.UnassignUser(dto);
        return Ok(new {
            result = "success"
        });
    }

    [HttpPost("delete")]
    public async Task<IActionResult> Delete([FromBody] TaskListObject dto)
    {
        await _service.DeleteAsync(dto);
        return Ok(new {
            result = "success"
        });
    }

}
