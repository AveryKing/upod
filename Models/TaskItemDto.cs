namespace ToDoApi.Models;

public class TaskItemDto
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public bool IsComplete { get; set; }
}