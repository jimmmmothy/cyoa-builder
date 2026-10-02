namespace Core.src
{
    public abstract class Condition
    {
        public required Variable Target { get; set; }
        public abstract bool IsMet(Dictionary<Guid, Variable> variablesByGuid);
    }
}