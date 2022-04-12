using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ToDoApi.Teams.Models;

public class Team
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public int Id { get; set; }
    [BsonElement("Name")]
    public string Name { get; set; }
    [BsonElement("Description")]
    public string? Description { get; set; }
    [BsonElement("ImageUrl")]
    public string? ImageUrl { get; set; }
    [BsonElement("Owner")]
    public string Owner { get; set; } 
    public ICollection<TeamMember> TeamMembers { get; set; }
    public ICollection<Task> Tasks { get; set; }
}