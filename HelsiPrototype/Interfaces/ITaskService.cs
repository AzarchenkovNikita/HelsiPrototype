using HelsiPrototype.DTO;
using HelsiPrototype.Model;

namespace HelsiPrototype.Interfaces;

public interface ITaskService
{
    Task<List<TaskEntity>> GetRangeAsync(TaskGet_Range dto);
    Task<TaskEntity> GetAsync(string id);
    Task<string> CreateAsync(Task_Add dto);
    Task<string> CreateAsync(string _Name, string _Description, string _OwnerId);
    Task UpdateAsync(Task_Upd dto);
    Task DeleteAsync(string id);
}
