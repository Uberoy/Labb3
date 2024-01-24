namespace Labb3WPF.Models;

public class CategoryModel
{
    public string Id { get; set; }

    private string _name { get; set; }
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }
}