using HelsiPrototype.DTO;
using HelsiPrototype.Model;

namespace HelsiPrototype.Interfaces;

public interface ITaskListService
{
    Task<string> CreateAsync(TaskList_Add dto);
    Task UpdateAsync(TaskList_Upd dto);
    Task<string> CreateTaskAsync(TaskList_CreateTask dto);
    Task AssignTask(TaskList_Link dto);
    Task UnassignTask(TaskList_Link dto);
    Task AssignUser(TaskList_Link dto);
    Task UnassignUser(TaskList_Link dto);
    Task DeleteAsync(TaskList_ dto);
    Task<TaskList_Response> GetAsync(TaskList_ dto);
    Task<List<TaskList>> GetRangeAsync(TaskList_GetRange dto);
}
