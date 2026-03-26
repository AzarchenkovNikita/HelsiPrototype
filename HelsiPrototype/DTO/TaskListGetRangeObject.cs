namespace HelsiPrototype.DTO;

public class TaskListGetRangeObject
{
    public string UserId { get; set; }
    public int Skip {  get; set; }
    public int Take { get; set; }
    public bool OrderByDesc { get; set; }
}
