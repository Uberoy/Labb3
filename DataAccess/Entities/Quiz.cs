using Common.DTO;
using MongoDB.Bson;

namespace Labb3.Entities;

public class Quiz
{
    public ObjectId Id { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }
    public List<Question> Questions { get; set; }
}