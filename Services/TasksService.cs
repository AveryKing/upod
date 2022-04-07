using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ToDoApi.Models;

namespace ToDoApi.Tasks;

public class TasksService
{
    private readonly IMongoCollection<ToDoItem> _tasksCollection;

    public TasksService(IOptions<TasksDatabaseSettings> tasksDatabaseSettings)
    {
        var mongoClient = new MongoClient(tasksDatabaseSettings.Value.ConnectionString);
        var database = mongoClient.GetDatabase(tasksDatabaseSettings.Value.DatabaseName);
        _tasksCollection = database.GetCollection<ToDoItem>(tasksDatabaseSettings.Value.TasksCollectionName);
    }
    
    public async Task<List<ToDoItem>> GetAsync()
    {
        return await _tasksCollection.Find(_ => true).ToListAsync();
    }
     
    public async Task<ToDoItem?> GetAsync(string id)
    {
        return await _tasksCollection.Find(task => task.Id == id).FirstOrDefaultAsync();
    }
    
    public async Task CreateAsync(ToDoItem task)
    {
        await _tasksCollection.InsertOneAsync(task);
    }
    
    public async Task UpdateAsync(string id, ToDoItem task)
    {
        var updateResult = await _tasksCollection.ReplaceOneAsync(item => item.Id == id, task);
    }
    
    public async Task DeleteAsync(string id)
    {
        var deleteResult = await _tasksCollection.DeleteOneAsync(item => item.Id == id);
    }
    
}