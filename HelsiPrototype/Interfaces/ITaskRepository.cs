using HelsiPrototype.Model;

namespace HelsiPrototype.Interfaces;

public interface ITaskRepository
{
    Task<List<TaskEntity>> GetList(int skip, int take, bool orderByDescending);
    Task<TaskEntity> GetAsync(string id);

    Task<List<TaskEntity>> GetRangeAsync(List<string> ids);

    Task CreateAsync(TaskEntity task);

    Task UpdateAsync(TaskEntity task);

    Task DeleteAsync(string id);
}
