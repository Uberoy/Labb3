namespace Labb3Console.Models;

public class QuestionModel
{
    public string Id { get; set; }
    private string _description { get; set; }

    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }

    private List<string> _answers { get; set; }

    public List<string> Answers
    {
        get { return _answers; }
        set { _answers = value; }
    }

    private int _correctAnswer { get; set; }

    public int CorrectAnswer
    {
        get { return _correctAnswer; }
        set { _correctAnswer = value; }
    }
}