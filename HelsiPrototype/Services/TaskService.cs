using HelsiPrototype.Interfaces;
using HelsiPrototype.Model;
using HelsiPrototype.DTO;

namespace HelsiPrototype.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;
    private readonly ITaskListRepository _taskListRepository;

    public TaskService(ITaskRepository repository, 
        ITaskListRepository taskListRepository)
    {
        _repository = repository;
        _taskListRepository = taskListRepository;
    }

    public async Task<List<TaskEntity>> GetRangeAsync(TaskGet_Range dto)
    {
        return await _repository.GetRangeAsync(dto.Skip, dto.Take, dto.OrderByDesc);
    }

    public async Task<TaskEntity> GetAsync(string id)
    {
        var task = await _repository.GetAsync(id);

        if (task == null)
            throw new Exception("Task not found");

        return task;
    }

    //оскільки сервіс може бути реюзабельним, для зручності створено перевантаження
    public async Task<string> CreateAsync(Task_Add dto)
    {
        return await CreateAsync(dto.Name, dto.Description, dto.UserId);
    }

    public async Task<string> CreateAsync(string _Name, string _Description, string _OwnerId)
    {
        if (_Name.Length > 255)
            throw new Exception("Name is too long");

        TaskEntity newTask = new TaskEntity()
        {
            Name = _Name,
            Description = _Description,
            OwnerId = _OwnerId
        };
        await _repository.CreateAsync(newTask);
        return newTask.Id;
    }

    public async Task UpdateAsync(Task_Upd dto)
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

        List<TaskList> taskListRange = await _taskListRepository.GetRangeAsync(id);
        taskListRange.ForEach(x => x.TaskIdList.Remove(id));
        await _taskListRepository.UpdateRangeAsync(taskListRange);

        await _repository.DeleteAsync(id);
    }
}
