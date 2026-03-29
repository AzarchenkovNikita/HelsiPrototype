namespace HelsiPrototype.DTO;

public class TaskGet_Range
{
    public int Skip { get; set; }
    public int Take { get; set; }
    public bool OrderByDesc { get; set; }
}
