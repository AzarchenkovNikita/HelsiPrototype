using HelsiPrototype.Model;

namespace HelsiPrototype.Interfaces;

public interface ITaskListRepository
{
    Task CreateAsync(TaskList taskList);
    Task UpdateAsync(TaskList taskList);
    Task UpdateRangeAsync(List<TaskList> taskLists);
    Task DeleteAsync(string id);
    Task<TaskList> GetAsync(string id);
    Task<List<TaskList>> GetRangeAsync(
        string userId, 
        int skip, 
        int take, 
        bool orderByDescending);
    Task<List<TaskList>> GetRangeAsync(string taskId);
}
