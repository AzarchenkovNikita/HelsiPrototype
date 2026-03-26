namespace HelsiPrototype.DTO;

public class TaskGetRangeObject
{
    public int Skip { get; set; }
    public int Take { get; set; }
    public bool OrderByDesc { get; set; }
}
