namespace Core.src
{
    public class BooleanVariable : Variable
    {
        public bool Value { get; set; }

        public override Variable Clone() => new BooleanVariable { Name = Name, Value = Value };
    }
}
