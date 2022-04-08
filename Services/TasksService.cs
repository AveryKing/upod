using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ToDoApi.Models;
using ToDoApi.Settings;
namespace ToDoApi.Services;

public class TasksService
{
    private readonly IMongoCollection<TaskItem> _tasksCollection;

    public TasksService(IOptions<TasksDbSettings> tasksDatabaseSettings)
    {
        var mongoClient = new MongoClient(tasksDatabaseSettings.Value.ConnectionString);
        var database = mongoClient.GetDatabase(tasksDatabaseSettings.Value.DatabaseName);
        _tasksCollection = database.GetCollection<TaskItem>(tasksDatabaseSettings.Value.TasksCollectionName);
    }
    
    public async Task<List<TaskItem>> GetAsync()
    {
        return await _tasksCollection.Find(_ => true).ToListAsync();
    }
     
    public async Task<TaskItem?> GetAsync(string id)
    {
        return await _tasksCollection.Find(task => task.Id == id).FirstOrDefaultAsync();
    }
    
    public async Task CreateAsync(TaskItem task)
    {
        await _tasksCollection.InsertOneAsync(task);
    }
    
    public async Task UpdateAsync(string id, TaskItem task)
    {
        await _tasksCollection.ReplaceOneAsync(item => item.Id == id, task);
    }
    
    public async Task DeleteAsync(string id)
    {
        await _tasksCollection.DeleteOneAsync(item => item.Id == id);
    }
    
}