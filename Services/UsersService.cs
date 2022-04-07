using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ToDoApi.Models;
using ToDoApi.Settings;

namespace ToDoApi.Services;

public class UsersService
{
    private readonly IMongoCollection<User> _usersCollection;

    public UsersService(IOptions<UsersDatabaseSettings> usersDatabaseSettings)
    {
        var mongoClient = new MongoClient(usersDatabaseSettings.Value.ConnectionString);
        var database = mongoClient.GetDatabase(usersDatabaseSettings.Value.DatabaseName);
        _usersCollection = database.GetCollection<User>(usersDatabaseSettings.Value.UsersCollectionName);
    }
    
    public async Task<List<User>> GetAsync()
    {
        return await _usersCollection.Find(_ => true).ToListAsync();
    }
     
    public async Task<User?> GetAsync(string id)
    {
        return await _usersCollection.Find(user => user.Id == id).FirstOrDefaultAsync();
    }   
    
    public async Task CreateAsync(User user)
    {
        await _usersCollection.InsertOneAsync(user);
    }
    
    public async Task UpdateAsync(string id, User user)
    {
        await _usersCollection.ReplaceOneAsync(u => u.Id == id, user);
    }
    
    public async Task DeleteAsync(string id)
    {
        await _usersCollection.DeleteOneAsync(user => user.Id == id);
    }
}

