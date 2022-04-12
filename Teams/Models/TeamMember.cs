using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ToDoApi.Users.Models;

namespace ToDoApi.Teams.Models;

public class TeamMember : User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string TeamId { get; set; }
    [BsonElement("Role")]
    public TeamRole Role { get; set; }
    
}  