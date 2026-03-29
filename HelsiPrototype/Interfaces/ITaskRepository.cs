using HelsiPrototype.Model;

namespace HelsiPrototype.Interfaces;

public interface ITaskRepository
{
    Task<TaskEntity> GetAsync(string id);
    Task<List<TaskEntity>> GetRangeAsync(int skip, int take, bool orderByDescending);
    Task<List<TaskEntity>> GetRangeAsync(List<string> idRange);

    Task CreateAsync(TaskEntity task);

    Task UpdateAsync(TaskEntity task);

    Task DeleteAsync(string id);
}
