using MongoDB.Bson;

namespace Labb3.Entities;

public class Category
{
    public ObjectId Id { get; set; }
    public string Name { get; set; }
}