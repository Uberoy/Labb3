namespace Labb3WPF.Models;

public class AnswerModel
{
    private string _text { get; set; }
    public string Text
    {
        get { return _text; }
        set { _text = value; }
    }
    private bool _isCorrect { get; set; }
    public bool IsCorrect
    {
        get { return _isCorrect; }
        set { _isCorrect = value; }
    }
}