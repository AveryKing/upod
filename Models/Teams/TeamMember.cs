using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ToDoApi.Models;

public class TeamMember : User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string TeamId { get; set; }
    [BsonElement("Role")]
    public TeamRole Role { get; set; }
    
}  