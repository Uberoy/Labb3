namespace Labb3WPF.Models;

public class QuizModel
{
    public string Id { get; set; }

    private string _name { get; set; }

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    private string _description { get; set; }

    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }
}