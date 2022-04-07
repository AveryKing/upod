namespace ToDoApi.Models;

public class TasksDatabaseSettings
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public string TasksCollectionName { get; set; }
}