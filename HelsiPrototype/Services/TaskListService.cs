using HelsiPrototype.DTO;
using HelsiPrototype.Interfaces;
using HelsiPrototype.Model;

namespace HelsiPrototype.Services;

public class TaskListService : ITaskListService
{
    private readonly ITaskListRepository _repository;
    private readonly IUserRepository _userRepository;
    private readonly ITaskRepository _taskRepository;

    public TaskListService(ITaskListRepository repository, 
        IUserRepository userRepository, 
        ITaskRepository taskRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
        _taskRepository = taskRepository;
    }

    public async Task<string> CreateAsync(TaskListAddObject taskListObject)
    {
        User verifiedUser = await _userRepository.GetAsync(taskListObject.UserId);
        if (verifiedUser is null)
            throw new Exception("User not exist");

        TaskList taskList = new TaskList()
        {
            Name = taskListObject.Name,
            Description = taskListObject.Description,
            OwnerId = taskListObject.UserId,
            UserIdList = new List<string>() { taskListObject.UserId }
        };

        await _repository.CreateAsync(taskList);

        return taskList.Id;
    }

    public async Task UpdateAsync(TaskListUpdObject dto)
    {
        var taskList = await GetAndCheckAccess(dto.TaskListId, dto.UserId);
        taskList.Name = dto.NewName;
        taskList.Description = dto.NewDescription;
        await _repository.UpdateAsync(taskList);
    }

    public async Task AssignTask(TaskListLinkObject dto)
    {
        var taskList = await GetAndCheckAccess(dto.TaskListId, dto.UserId);

        if (taskList.TaskIdList.Contains(dto.EntityId))
            throw new Exception("Already assigned");

        TaskEntity taskToAssign = await _taskRepository.GetAsync(dto.EntityId);
        if (taskToAssign is null)
            throw new Exception("Task to assign not exist");

        taskList.TaskIdList.Add(dto.EntityId);
        await _repository.UpdateAsync(taskList);
    }

    public async Task UnassignTask(TaskListLinkObject dto)
    {
        var taskList = await GetAndCheckAccess(dto.TaskListId, dto.UserId);

        taskList.TaskIdList.Remove(dto.EntityId);
        await _repository.UpdateAsync(taskList);
    }

    public async Task AssignUser(TaskListLinkObject dto)
    {
        var taskList = await GetAndCheckAccess(dto.TaskListId, dto.UserId);

        if (taskList.UserIdList.Contains(dto.EntityId))
            throw new Exception("Already assigned");

        User userToAssign = await _userRepository.GetAsync(dto.EntityId);
        if (userToAssign is null)
            throw new Exception("User to assign not exist");

        taskList.UserIdList.Add(dto.EntityId);
        await _repository.UpdateAsync(taskList);
    }

    public async Task UnassignUser(TaskListLinkObject dto)
    {
        var taskList = await GetAndCheckAccess(dto.TaskListId, dto.UserId);

        if (taskList.OwnerId.Equals(dto.EntityId))
            throw new Exception("Owner can't be unassigned");

        taskList.UserIdList.Remove(dto.EntityId);
        await _repository.UpdateAsync(taskList);
    }

    public async Task DeleteAsync(TaskListObject dto)
    {
        var taskList = await GetAndCheckAccess(dto.TaskListId, dto.UserId);
        if (!taskList.OwnerId.Equals(dto.UserId))
            throw new Exception("Only owner can delete taskList");

        await _repository.DeleteAsync(taskList.Id);
    }

    public async Task<TaskListResponse> GetAsync(TaskListObject dto)
    {
        TaskList taskList = await GetAndCheckAccess(dto.TaskListId, dto.UserId);
        
        var taskData = await _taskRepository.GetRangeAsync(taskList.TaskIdList);

        return new()
        {
            TaskList = taskList,
            TaskData = taskData
        };
    }

    public async Task<List<TaskList>> GetRangeAsync(TaskListGetRangeObject dto)
    {
        return await _repository.GetRangeAsync(dto.UserId, dto.Skip, dto.Take, dto.OrderByDesc);
    }

    private async Task<TaskList> GetAndCheckAccess(string taskListId, string userId)
    {
        var taskList = await _repository.GetAsync(taskListId);

        if (taskList == null)
            throw new Exception("TaskList not found");

        if (!taskList.UserIdList.Contains(userId))
            throw new Exception("Access denied");

        return taskList;
    }
}
