namespace cyoa_core.src
{
    class NumericVariable : Variable
    {
        public double Value { get; set; }

        public override Variable Clone() => new NumericVariable { Name = Name, Value = Value };
    }
}
