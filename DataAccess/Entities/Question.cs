using MongoDB.Bson;

namespace Labb3.Entities;

public class Question
{
    public ObjectId Id { get; set; }
    public string Description { get; set; }
    public List<string> Answers { get; set; }
    public int CorrectAnswer { get; set; }
    public List<Category> Categories { get; set; }
}