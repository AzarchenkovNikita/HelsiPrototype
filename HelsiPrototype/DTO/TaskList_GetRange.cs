namespace HelsiPrototype.DTO;

public class TaskList_GetRange
{
    public string UserId { get; set; }
    public int Skip {  get; set; }
    public int Take { get; set; }
    public bool OrderByDesc { get; set; }
}
