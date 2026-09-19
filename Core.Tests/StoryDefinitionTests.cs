using cyoa_core.src;

namespace cyoa_core_tests;

public class StoryDefinitionTests
{
    [Fact]
    public void NodeToChapter_MapsEveryNodeToItsOwnChapter()
    {
        Page a = new("A");
        Page b = new("B");
        Chapter chapter1 = TestUtilities.MakeChapter("Chapter 1", 0, a);
        Chapter chapter2 = TestUtilities.MakeChapter("Chapter 2", 1, b);

        StoryDefinition sd = new(a, [chapter1, chapter2]);

        Assert.Equal(chapter1, sd.NodeToChapter[a]);
        Assert.Equal(chapter2, sd.NodeToChapter[b]);
    }

    [Fact]
    public void Start_IsAddedAsEntryNodeOfItsOwnChapter()
    {
        Page start = new("Start");
        Chapter chapter1 = TestUtilities.MakeChapter("Chapter 1", 0, start);

        StoryDefinition sd = new(start, [chapter1]);

        Assert.Contains(start, chapter1.EntryNodes);
    }

    [Fact]
    public void PageNext_WithinSameChapter_IsNotTaggedAsEntryNode()
    {
        Page next = new("Next");
        Page start = new("Start", next);
        Chapter chapter1 = TestUtilities.MakeChapter("Chapter 1", 0, start, next);

        StoryDefinition sd = new(start, [chapter1]);

        Assert.DoesNotContain(next, chapter1.EntryNodes);
    }

    [Fact]
    public void PageNext_CrossingIntoAnotherChapter_IsTaggedAsEntryNode()
    {
        Page next = new("Next");
        Page start = new("Start", next);
        Chapter chapter1 = TestUtilities.MakeChapter("Chapter 1", 0, start);
        Chapter chapter2 = TestUtilities.MakeChapter("Chapter 2", 1, next);

        StoryDefinition sd = new(start, [chapter1, chapter2]);

        Assert.Contains(next, chapter2.EntryNodes);
    }

    [Fact]
    public void BranchChoice_WithinSameChapter_IsNotTaggedAsEntryNode()
    {
        Page target = new("Target");
        Branch branch = new();
        branch.Choices.Add(new Choice { Label = "1", Next = target });
        Chapter chapter1 = TestUtilities.MakeChapter("Chapter 1", 0, branch, target);

        StoryDefinition sd = new(branch, [chapter1]);

        Assert.DoesNotContain(target, chapter1.EntryNodes);
    }

    [Fact]
    public void BranchChoices_CrossingIntoAnotherChapter_IsTaggedAsEntryNode()
    {
        Page target1 = new("Target 1");
        Page target2 = new("Target 2");
        Branch branch = new();
        branch.Choices.Add(new Choice { Label = "1", Next = target1 });
        branch.Choices.Add(new Choice { Label = "2", Next = target2 });
        Chapter chapter1 = TestUtilities.MakeChapter("Chapter 1", 0, branch);
        Chapter chapter2 = TestUtilities.MakeChapter("Chapter 2", 1, target1, target2);

        StoryDefinition sd = new(branch, [chapter1, chapter2]);

        Assert.Contains(target1, chapter2.EntryNodes);
        Assert.Contains(target2, chapter2.EntryNodes);
    }

    [Fact]
    public void ConditionalBranch_RouteCrossingIntoAnotherChapter_IsTaggedAsEntryNode()
    {
        Page routeTarget = new("Route target");
        Page defaultTarget = new("Default target");
        ConditionalBranch condBranch = new(defaultTarget);
        BooleanVariable variable = new() { Name = "Flag" };
        BooleanCondition condition = new() { Target = variable, Expected = true };
        condBranch.Routes.Add(new ConditionalRoute(condition, routeTarget));
        Chapter chapter1 = TestUtilities.MakeChapter("Chapter 1", 0, condBranch, defaultTarget);
        Chapter chapter2 = TestUtilities.MakeChapter("Chapter 2", 1, routeTarget);

        StoryDefinition sd = new(condBranch, [chapter1, chapter2]);

        Assert.Contains(routeTarget, chapter2.EntryNodes);
        Assert.DoesNotContain(defaultTarget, chapter1.EntryNodes);
    }

    [Fact]
    public void ConditionalBranch_DefaultCrossingIntoAnotherChapter_IsTaggedAsEntryNode()
    {
        Page defaultTarget = new("Default target");
        ConditionalBranch condBranch = new(defaultTarget);
        Chapter chapter1 = TestUtilities.MakeChapter("Chapter 1", 0, condBranch);
        Chapter chapter2 = TestUtilities.MakeChapter("Chapter 2", 1, defaultTarget);

        StoryDefinition sd = new(condBranch, [chapter1, chapter2]);

        Assert.Contains(defaultTarget, chapter2.EntryNodes);
    }
}
