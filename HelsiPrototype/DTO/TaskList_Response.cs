namespace HelsiPrototype.DTO;

using HelsiPrototype.Model;

public class TaskList_Response
{
    public TaskList TaskList { get; set; } = new();
    public List<TaskEntity> TaskData { get; set; } = new();
}
