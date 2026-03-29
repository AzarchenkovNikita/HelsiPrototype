using HelsiPrototype.DTO;
using HelsiPrototype.Interfaces;
using HelsiPrototype.Model;

namespace HelsiPrototype.Services;

public class TaskListService : ITaskListService
{
    private readonly ITaskListRepository _repository;
    private readonly IUserRepository _userRepository;
    private readonly ITaskRepository _taskRepository;

    private readonly ITaskService _taskService;

    public TaskListService(ITaskListRepository repository, 
        IUserRepository userRepository, 
        ITaskRepository taskRepository,
        ITaskService taskService)
    {
        _repository = repository;
        _userRepository = userRepository;
        _taskRepository = taskRepository;

        _taskService = taskService;
    }

    public async Task<string> CreateAsync(TaskList_Add dto)
    {
        if (dto.Name.Length > 255)
            throw new Exception("Name is too long");

        User verifiedUser = await _userRepository.GetAsync(dto.UserId);
        if (verifiedUser is null)
            throw new Exception("User not exist");

        TaskList taskList = new TaskList()
        {
            Name = dto.Name,
            Description = dto.Description,
            OwnerId = dto.UserId,
            UserIdList = new List<string>() { dto.UserId }
        };

        await _repository.CreateAsync(taskList);

        return taskList.Id;
    }

    public async Task UpdateAsync(TaskList_Upd dto)
    {
        var taskList = await GetAndCheckAccess(dto.TaskListId, dto.UserId);
        taskList.Name = dto.NewName;
        taskList.Description = dto.NewDescription;
        await _repository.UpdateAsync(taskList);
    }

    // юзер може створити задачу і одразу додати її в список задач, якщо потрібно
    // використовуючи один атомарний сервіс
    public async Task<string> CreateTaskAsync(TaskList_CreateTask dto)
    {
        var taskList = await GetAndCheckAccess(dto.TaskListId, dto.UserId);

        if (!taskList.UserIdList.Contains(dto.UserId))
            throw new Exception("Task owner is not a member of the task list, " +
                "please assign him first");

        string taskId = await _taskService.CreateAsync(dto.Name, dto.Description, dto.UserId);
        taskList.TaskIdList.Add(taskId);
        await _repository.UpdateAsync(taskList);

        return taskId;
    }

    // юзер може приєднати існуючу задачу
    public async Task AssignTask(TaskList_Link dto)
    {
        var taskList = await GetAndCheckAccess(dto.TaskListId, dto.UserId);

        if (taskList.TaskIdList.Contains(dto.EntityId))
            throw new Exception("Already assigned");

        TaskEntity taskToAssign = await _taskRepository.GetAsync(dto.EntityId);
        if (taskToAssign is null)
            throw new Exception("Task to assign not exist");

        if (!taskList.UserIdList.Contains(taskToAssign.OwnerId))
            throw new Exception("Task owner is not a member of the task list, " +
                "please assign him first");

        taskList.TaskIdList.Add(dto.EntityId);
        await _repository.UpdateAsync(taskList);
    }

    public async Task UnassignTask(TaskList_Link dto)
    {
        var taskList = await GetAndCheckAccess(dto.TaskListId, dto.UserId);

        taskList.TaskIdList.Remove(dto.EntityId);
        await _repository.UpdateAsync(taskList);
    }

    public async Task AssignUser(TaskList_Link dto)
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

    public async Task UnassignUser(TaskList_Link dto)
    {
        var taskList = await GetAndCheckAccess(dto.TaskListId, dto.UserId);

        // овнер не може бути видалений зі списку учасників
        // це нівелює потребу звернення до OwnerId в кожному методі
        if (taskList.OwnerId.Equals(dto.EntityId))
            throw new Exception("Owner can't be unassigned");

        taskList.UserIdList.Remove(dto.EntityId);
        await _repository.UpdateAsync(taskList);
    }

    public async Task DeleteAsync(TaskList_ dto)
    {
        var taskList = await GetAndCheckAccess(dto.TaskListId, dto.UserId);
        if (!taskList.OwnerId.Equals(dto.UserId))
            throw new Exception("Only owner can delete taskList");

        await _repository.DeleteAsync(taskList.Id);
    }

    // тут також присутня інформація по звязках із юзерами
    public async Task<TaskList_Response> GetAsync(TaskList_ dto)
    {
        TaskList taskList = await GetAndCheckAccess(dto.TaskListId, dto.UserId);
        
        var taskData = await _taskRepository.GetRangeAsync(taskList.TaskIdList);

        return new()
        {
            TaskList = taskList,
            TaskData = taskData
        };
    }

    public async Task<List<TaskList>> GetRangeAsync(TaskList_GetRange dto)
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
