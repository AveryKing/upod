namespace ToDoApi.Settings;

public class TasksDbSettings
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public string TasksCollectionName { get; set; }
}