using HelsiPrototype.DTO;
using HelsiPrototype.Model;

namespace HelsiPrototype.Interfaces;

public interface ITaskListService
{
    Task<string> CreateAsync(TaskListAddObject taskListObject);
    Task UpdateAsync(TaskListUpdObject taskListUpdObject);
    Task AssignTask(TaskListLinkObject dto);
    Task UnassignTask(TaskListLinkObject dto);
    Task AssignUser(TaskListLinkObject dto);
    Task UnassignUser(TaskListLinkObject dto);
    Task DeleteAsync(TaskListObject taskListDeleteObject);
    Task<TaskListResponse> GetAsync(TaskListObject dto);
    Task<List<TaskList>> GetRangeAsync(TaskListGetRangeObject dto);
}
