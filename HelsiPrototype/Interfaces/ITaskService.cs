using HelsiPrototype.DTO;
using HelsiPrototype.Model;

namespace HelsiPrototype.Interfaces;

public interface ITaskService
{
    Task<List<TaskEntity>> GetRangeAsync(TaskGetRangeObject dto);
    Task<TaskEntity> GetAsync(string id);
    Task<string> CreateAsync(TaskObject dto);
    Task UpdateAsync(TaskUpdObject dto);
    Task DeleteAsync(string id);
}
