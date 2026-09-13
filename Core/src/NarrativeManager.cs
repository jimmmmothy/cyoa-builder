using cyoa_core.src.exceptions;

namespace cyoa_core.src
{
    class NarrativeManager
    {
        private NarrativeManager(StoryState ss, StoryDefinition sd, Node current)
        {
            StoryState = ss;
            StoryDefinition = sd;
            Current = current;    
        }

        public StoryState StoryState { get; }
        public StoryDefinition StoryDefinition { get; set; }
        public Node Current { get; set; }

        public static NarrativeManager NewGame(StoryDefinition storyDefinition)
        {
            var storyState = new StoryState();
            var manager = new NarrativeManager(storyState, storyDefinition, storyDefinition.Start);
            manager.RecordInitialCheckpoint();
            return manager;
        }

        public static NarrativeManager Resume(StoryState storyState, StoryDefinition storyDefinition, Node current) => new(storyState, storyDefinition, current);

        public bool CheckCondition(Condition condition)
        {
            return condition.IsMet(StoryState);
        }

        public void Advance()
        {
            if (Current is not Page p)
            {
                throw new InvalidOperationException("Current node is not a Page, use SelectChoice(Choice) instead");
            }

            if (p.Next == null)
                throw new NoNextException("This page has no Next");

            var previous = Current;
            Current = p.Next;
            OnNext(previous);
        }

        public void SelectChoice(Choice choice)
        {
            if (Current is not Branch b)
            {
                throw new InvalidOperationException("Current node is not a Branch, use Advance() instead");
            }

            if (!b.Choices.Contains(choice))
                throw new ArgumentException("This Choice does not come from this Branch");

            if (choice.Gate != null && !choice.Gate.IsMet(StoryState))
            {
                throw new ConditionNotMetException();
            }

            var previous = Current;
            choice.ApplyEffects(StoryState);
            Current = choice.Next;
            OnNext(previous);
        }

        private void RecordInitialCheckpoint()
        {
            StoryState.Checkpoints.Add((Chapter)StoryDefinition.Chapters.OrderBy(c => c.Order), new ChapterCheckpoint(StoryDefinition.Start, [.. StoryState.Variables.Values]));
        }

        private void OnNext(Node previous)
        {
            ResolveConditionalBranch();
            HandleChapterChange(previous);
            HandlePageEffects();
            StoryState.History.Push(Current);
        }

        private void ResolveConditionalBranch()
        {
            if (Current is not ConditionalBranch cb)
                return;

            foreach (ConditionalRoute route in cb.Routes)
            {
                // Designed to pass on the first matching condition, so order is very important
                if (CheckCondition(route.When))
                {
                    Current = route.Then;
                    // Recursive in case the following node is also a conditional branch
                    ResolveConditionalBranch();
                    return;
                }
            }

            Current = cb.Default;
            // Recursive in case the following node is also a conditional branch
            ResolveConditionalBranch();
        }

        private void HandleChapterChange(Node previous)
        {
            var currChapter = StoryDefinition.NodeToChapter[Current];
            if (StoryDefinition.NodeToChapter[previous] != StoryDefinition.NodeToChapter[Current])
            {
                // Generate a checkpoint
                StoryState.Checkpoints[currChapter] = new ChapterCheckpoint(Current, [.. StoryState.Variables.Values]);
            }
        }

        private void HandlePageEffects()
        {
            if (Current is Page p)
            {
                p.ApplyEffects(StoryState);
            }
        }
    }
}
