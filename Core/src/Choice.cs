namespace cyoa_core.src
{
    class Choice : IHaveEffects
    {
        public required string Label { get; set; }
        public Condition? Gate { get; set; }
        public required Node Next { get; set; }
        public List<Effect> Effects { get; } = [];

        public void ApplyEffects(StoryState ss)
        {
            foreach (var effect in Effects) {
                effect.Apply(ss);
            }
        }
    }
}