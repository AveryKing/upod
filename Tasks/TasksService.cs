using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ToDoApi.Settings;
using ToDoApi.Tasks.Models;

namespace ToDoApi.Tasks;

public class TasksService
{
    private readonly IMongoCollection<TaskItem> _tasksCollection;

    public TasksService(IOptions<TasksDbSettings> tasksDatabaseSettings)
    {
        var mongoClient = new MongoClient(tasksDatabaseSettings.Value.ConnectionString);
        var database = mongoClient.GetDatabase(tasksDatabaseSettings.Value.DatabaseName);
        _tasksCollection = database.GetCollection<TaskItem>(tasksDatabaseSettings.Value.TasksCollectionName);
    }

    public async Task<List<TaskItem>> GetAsync(string userId)
    {
        var filter = Builders<TaskItem>.Filter.Eq(t => t.Owner, userId);
        return await _tasksCollection.Find(filter).ToListAsync();
    }

    public async Task<TaskItem?> GetAsync(string taskId, string userId)
    {
        var filter = Builders<TaskItem>.Filter.Eq(t => t.Id, taskId)
                     & Builders<TaskItem>.Filter.Eq(t => t.Owner, userId);
        return await _tasksCollection.Find(filter).FirstOrDefaultAsync();
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