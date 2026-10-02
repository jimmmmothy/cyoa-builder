namespace Core.src
{
    public abstract class Effect 
    {
        public required Variable Target { get; set; }
        public abstract void Apply(Dictionary<Guid, Variable> variablesByGuid);
    }
}
