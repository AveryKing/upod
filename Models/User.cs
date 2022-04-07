using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ToDoApi.Models;

public class User
{
    [BsonRepresentation((BsonType.ObjectId))]
    [BsonId]
    public string? Id { get; set; }
    public string? Username { get; set; }
    
    public string? Email { get; set; }
    public string? Password { get; set; }
    
}