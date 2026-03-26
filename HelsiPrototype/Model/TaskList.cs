namespace HelsiPrototype.Model;

public class TaskList
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string OwnerId { get; set; } = string.Empty;
    public List<string> TaskIdList { get; set; } = new();
    public List<string> UserIdList { get; set; } = new();
}
