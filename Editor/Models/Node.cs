namespace Editor.Models;

public class Node
{
    public Node()
    {
        Title = "New node";
    }

    public Node(string title)
    {
        Title = title;
    }

    public string Title { get; set; }
}