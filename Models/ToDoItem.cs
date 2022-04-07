using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ToDoApi.Models;

public class ToDoItem
{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    [BsonElement("Name")]
    public string? Name { get; set; }
    public bool IsComplete { get; set; }
    public string? Secret { get; set; }


}
