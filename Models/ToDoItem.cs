namespace ToDoApi.Models;

public class ToDoItem
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public bool isComplete { get; set; }
    
}