namespace ToDoApi.Settings;

public class UsersSettings
{
    public string UsersCollectionName { get; set; }
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public string JwtSecret { get; set; }
}