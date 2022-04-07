namespace ToDoApi.Models;

public class ToDoItemDTO
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public bool IsComplete { get; set; }
}