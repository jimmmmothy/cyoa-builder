using cyoa_core.src;

namespace cyoa_core_tests;

public class TestUtilities
{
    public static StoryState CreateStoryState()
    {
        StoryState ss = new();
        for (int i = 0; i < 5; i++)
        {
            if (i % 2 == 0)
            {
                var boolVar = new BooleanVariable { Name = $"Boolean {i}", Value = true };
                ss.Variables[boolVar.Guid] = boolVar;
            }
            else
            {
                var numVar = new NumericVariable { Name = $"Numeric {i}", Value = i };
                ss.Variables[numVar.Guid] = numVar;
            }
        }
        return ss;
    }

    // Normally, these variables would be references to those in StoryDefinition.VariableDeclarations,
    // but for testing purposes it's easier to get them from the StoryState since I need it while I don't need the
    // StoryDefinition (yet), and the variables are useful only for their Guid anyways.
    // When I make tests with StoryDefinition I'll probably change this.
    public static T GetVar<T>(StoryState ss, string name) where T : Variable
    {
        return (T)ss.Variables.First(kvp => kvp.Value.Name == name).Value;
    }

    public static Chapter MakeChapter(string title, uint order, params Node[] nodes)
    {
        Chapter chapter = new() { Title = title, Order = order };
        chapter.Nodes.AddRange(nodes);
        return chapter;
    }
}