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
    public async Task<IActionResult> Create([FromBody] TaskList_Add dto)
    {
        string taskListId = await _service.CreateAsync(dto);
        return Ok(new { taskListId });
    }

    [HttpPost("get")]
    public async Task<IActionResult> Get([FromBody] TaskList_ dto)
    {
        TaskList_Response response = await _service.GetAsync(dto);
        return Ok(new { response });
    }

    [HttpPost("getrange")]
    public async Task<IActionResult> GetRange([FromBody] TaskList_GetRange dto)
    {
        List<TaskList> taskListRange = await _service.GetRangeAsync(dto);
        return Ok(new { taskListRange });
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update([FromBody] TaskList_Upd dto)
    {
        await _service.UpdateAsync(dto);
        return Ok(new { 
            result = "success"
        });
    }

    [HttpPost("createtask")]
    public async Task<IActionResult> CreateTask([FromBody] TaskList_CreateTask dto)
    {
        string taskId = await _service.CreateTaskAsync(dto);
        return Ok(new { taskId });
    }

    [HttpPost("assigntask")]
    public async Task<IActionResult> AssignTask([FromBody] TaskList_Link dto)
    {
        await _service.AssignTask(dto);
        return Ok(new {
            result = "success"
        });
    }

    [HttpPost("unassigntask")]
    public async Task<IActionResult> UnassignTask([FromBody] TaskList_Link dto)
    {
        await _service.UnassignTask(dto);
        return Ok(new {
            result = "success"
        });
    }

    [HttpPost("assignuser")]
    public async Task<IActionResult> AssignUser([FromBody] TaskList_Link dto)
    {
        await _service.AssignUser(dto);
        return Ok(new {
            result = "success"
        });
    }

    [HttpPost("unassignuser")]
    public async Task<IActionResult> UnassignUser([FromBody] TaskList_Link dto)
    {
        await _service.UnassignUser(dto);
        return Ok(new {
            result = "success"
        });
    }

    [HttpPost("delete")]
    public async Task<IActionResult> Delete([FromBody] TaskList_ dto)
    {
        await _service.DeleteAsync(dto);
        return Ok(new {
            result = "success"
        });
    }

}
