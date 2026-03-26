using HelsiPrototype.Interfaces;
using HelsiPrototype.Model;
using HelsiPrototype.DTO;

namespace HelsiPrototype.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<TaskEntity>> GetRangeAsync(TaskGetRangeObject dto)
    {
        return await _repository.GetList(dto.Skip, dto.Take, dto.OrderByDesc);
    }

    public async Task<TaskEntity> GetAsync(string id)
    {
        var task = await _repository.GetAsync(id);

        if (task == null)
            throw new Exception("Task not found");

        return task;
    }

    public async Task<string> CreateAsync(TaskObject dto)
    {
        TaskEntity newTask = new TaskEntity()
        {
            Name = dto.Name,
            Description = dto.Description
        };
        await _repository.CreateAsync(newTask);
        return newTask.Id;
    }

    public async Task UpdateAsync(TaskUpdObject dto)
    {
        TaskEntity existing = await _repository.GetAsync(dto.TaskId);

        if (existing is null)
            throw new Exception("Task not found");

        existing.Name = dto.Name;
        existing.Description = dto.Description;
        await _repository.UpdateAsync(existing);
    }

    public async Task DeleteAsync(string id)
    {
        TaskEntity existing = await _repository.GetAsync(id);

        if (existing == null)
            throw new Exception("Task not found");

        await _repository.DeleteAsync(id);
    }
}
