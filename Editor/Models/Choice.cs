namespace Editor.Models;

public class Choice
{
    public Choice()
    {
        Label = "New choice";
    }

    public string Label { get; set; }
    public Node? Next { get; set; }
}