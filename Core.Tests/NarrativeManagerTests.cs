using cyoa_core.src;
using cyoa_core.src.exceptions;

namespace cyoa_core_tests;

public class NarrativeManagerTests
{
    private StoryDefinition CreateGenericStoryDefinition(Node start, List<Node> nodesInChap1, List<Node> nodesInChap2)
    {
        Chapter chapter1 = TestUtilities.MakeChapter("Chapter 1", 0, [..nodesInChap1]);
        Chapter chapter2 = TestUtilities.MakeChapter("Chapter 2", 1, [..nodesInChap2]);

        return new StoryDefinition(start, [chapter1, chapter2]);
    }

    [Fact]
    public void NarrativeManager_NewGameRecordsInitialCheckpointCorrectly()
    {
        Page start = new("Start");
        Page next = new("Next");
        start.Next = next;

        var sd = CreateGenericStoryDefinition(start, [start], [next]);
        var nm = NarrativeManager.NewGame(sd);

        Assert.Single(nm.StoryState.Checkpoints);
        Assert.Equal(nm.StoryDefinition.Start, nm.StoryState.Checkpoints[nm.StoryDefinition.Chapters[0]].EntryNode);
    }

    [Fact]
    public void NarrativeManager_ResumeCreatesIdenticalNarrative()
    {
        Page start = new("Start");
        Page next = new("Next");
        start.Next = next;

        var sd = CreateGenericStoryDefinition(start, [start], [next]);
        var nm1 = NarrativeManager.NewGame(sd);
        var nm2 = NarrativeManager.Resume(nm1.StoryState, sd, sd.Start);

        Assert.Equal(nm1.StoryState, nm2.StoryState);
        Assert.Equal(nm1.StoryDefinition, nm2.StoryDefinition);
        Assert.Equal(nm1.Current, nm2.Current);
    }

    [Fact]
    public void Advance_ShouldSetNext()
    {
        Page start = new("Start");
        Page next = new("Next");
        start.Next = next;

        var sd = CreateGenericStoryDefinition(start, [start], [next]);
        var nm = NarrativeManager.NewGame(sd);
        nm.Advance();

        Assert.Equal(next, nm.Current);
    }

    [Fact]
    public void Advance_ShouldThrow_WhenNotPage()
    {
        Branch start = new();
        Page next = new("Next");
        Choice choice = new() { Label = "Choice 1", Next = next };
        start.Choices.Add(choice);
        
        var sd = CreateGenericStoryDefinition(start, [start], [next]);
        var nm = NarrativeManager.NewGame(sd);

        Assert.Throws<InvalidOperationException>(nm.Advance);
    }

    [Fact]
    public void Advance_ShouldThrow_WhenNoNext()
    {
        Page start = new("Start");
        var sd = CreateGenericStoryDefinition(start, [start], []);
        var nm = NarrativeManager.NewGame(sd);

        Assert.Throws<NoNextException>(nm.Advance);
    }

    [Fact]
    public void SelectChoice_ShouldApplyChoiceEffects()
    {
        Branch start = new();
        Page next = new("Next");
        BooleanVariable variable = new() { Name = "Var", Value = false };
        BooleanEffect effect = new() { Target = variable, Op = BooleanOp.TOGGLE };
        Choice choice = new() { Label = "Choice 1", Next = next, Effects = [effect]};
        start.Choices.Add(choice);
        
        var sd = CreateGenericStoryDefinition(start, [start], [next]);
        var nm = NarrativeManager.NewGame(sd);
        nm.StoryState.Variables[variable.Guid] = variable;
        nm.SelectChoice(choice);

        Assert.True(variable.Value);
        Assert.Equal(next, nm.Current);
    }

    [Fact]
    public void SelectChoice_ShouldThrow_WhenNotBranch()
    {
        Page start = new("Start");
        Page next = new("Next");
        BooleanVariable variable = new() { Name = "Var", Value = false };
        BooleanEffect effect = new() { Target = variable, Op = BooleanOp.TOGGLE };
        Choice choice = new() { Label = "Choice 1", Next = next, Effects = [effect]};

        var sd = CreateGenericStoryDefinition(start, [start], []);
        var nm = NarrativeManager.NewGame(sd);

        Assert.Throws<InvalidOperationException>(() => nm.SelectChoice(choice));
    }

    [Fact]
    public void SelectChoice_ShouldThrow_WhenChoiceIsInvalid()
    {
        Branch start = new();
        Page next = new("Next");
        BooleanVariable variable = new() { Name = "Var", Value = false };
        BooleanEffect effect = new() { Target = variable, Op = BooleanOp.TOGGLE };
        Choice choice = new() { Label = "Choice 1", Next = next, Effects = [effect]};
        
        var sd = CreateGenericStoryDefinition(start, [start], [next]);
        var nm = NarrativeManager.NewGame(sd);

        Assert.Throws<ArgumentException>(() => nm.SelectChoice(choice));
    }

    [Fact]
    public void SelectChoice_ShouldThrow_WhenConditionIsNotMet()
    {
        Branch start = new();
        Page next = new("Next");
        BooleanVariable variable = new() { Name = "Var", Value = false };
        BooleanEffect effect = new() { Target = variable, Op = BooleanOp.TOGGLE };
        BooleanCondition condition = new() { Target = variable, Expected = true };
        Choice choice = new() { Label = "Choice 1", Next = next, Effects = [effect], Gate = condition };
        start.Choices.Add(choice);
        
        var sd = CreateGenericStoryDefinition(start, [start], [next]);
        var nm = NarrativeManager.NewGame(sd);
        nm.StoryState.Variables[variable.Guid] = variable;

        Assert.Throws<ConditionNotMetException>(() => nm.SelectChoice(choice));
    }

    [Fact]
    public void OnNext_ShouldResolveConditionalBranch()
    {
        Page start = new("Start");
        Page defaultNext = new("Default");
        ConditionalBranch cb = new(defaultNext);
        start.Next = cb;

        BooleanVariable variable = new() { Name = "Var", Value = false };
        BooleanCondition when = new() { Target = variable, Expected = false };
        Page then = new("Then");
        ConditionalRoute cr = new(when, then);
        cb.Routes.Add(cr);

        var sd = CreateGenericStoryDefinition(start, [start], [cb, defaultNext, then]);
        var nm = NarrativeManager.NewGame(sd);
        nm.StoryState.Variables[variable.Guid] = variable;
        nm.Advance();

        Assert.Equal(then, nm.Current);
    }

    [Fact]
    public void OnNext_ShouldResolveConsecutiveConditionalBranches()
    {
        Page start = new("Start");
        Page defaultNext = new("Default");
        Page defaultNext2 = new("Default");
        ConditionalBranch cb1 = new(defaultNext);
        ConditionalBranch cb2 = new(defaultNext2);
        start.Next = cb1;

        BooleanVariable variable = new() { Name = "Var", Value = false };
        BooleanCondition when = new() { Target = variable, Expected = false };
        ConditionalRoute cr1 = new(when, cb2);
        cb1.Routes.Add(cr1);

        var sd = CreateGenericStoryDefinition(start, [start], [cb1, cb2, defaultNext, defaultNext2]);
        var nm = NarrativeManager.NewGame(sd);
        nm.StoryState.Variables[variable.Guid] = variable;
        nm.Advance();

        Assert.Equal(defaultNext2, nm.Current);
    }

    [Fact]
    public void OnNext_ShouldHandleChapterChange()
    {
        Page start = new("Start");
        Page next = new("Next");
        start.Next = next;

        var sd = CreateGenericStoryDefinition(start, [start], [next]);
        var nm = NarrativeManager.NewGame(sd);
        nm.Advance();

        Assert.Equal(2, nm.StoryState.Checkpoints.Count);
    }

    [Fact]
    public void OnNext_ShouldHandlePageEffects()
    {
        Page start = new("Start");
        Page next = new("Next");
        start.Next = next;
        BooleanVariable variable = new() { Name = "Var", Value = false };
        BooleanEffect effect = new() { Target = variable, Op = BooleanOp.TOGGLE };
        next.Effects.Add(effect);

        var sd = CreateGenericStoryDefinition(start, [start], [next]);
        var nm = NarrativeManager.NewGame(sd);
        nm.StoryState.Variables[variable.Guid] = variable;
        nm.Advance();

        Assert.True(variable.Value);
    }

    [Fact]
    public void OnNext_ShouldPushToHistory()
    {
        Page start = new("Start");
        Page next = new("Next");
        start.Next = next;

        var sd = CreateGenericStoryDefinition(start, [start], [next]);
        var nm = NarrativeManager.NewGame(sd);
        nm.Advance();

        Assert.Equal(2, nm.StoryState.History.Count);
    }
}