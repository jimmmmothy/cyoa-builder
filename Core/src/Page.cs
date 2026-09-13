namespace cyoa_core.src
{
    class Page : Node, IHaveEffects
    {
        public Page(string content)
        {
            Content = content;
            Next = null;
            Effects = [];
        }

        public Page(string content, Node next)
        {
            Content = content;
            Next = next;
            Effects = [];
        }

        public Page(string content, Node next, List<Effect> effects)
        {
            Content = content;
            Next = next;
            Effects = effects;
        }

        public string Content { get; set; }
        public Node? Next { get; set; }
        public List<Effect> Effects { get; set; }

        public void ApplyEffects(StoryState ss)
        {
            foreach (var effect in Effects) {
                effect.Apply(ss);
            }
        }
    }
}
