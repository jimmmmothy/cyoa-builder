using cyoa_core.src.exceptions;

namespace cyoa_core.src
{
    class NumericEffect : Effect
    {
        public NumericOp Op { get; set; }
        public double Value { get; set; }

        public override void Apply(StoryState ss)
        {
            var variable = ss.Variables[Target.Name] ?? throw new ArgumentNullException();

            if (variable is NumericVariable numVar)
            {
                numVar.Value = Op switch
                {
                    NumericOp.SET => Value,
                    NumericOp.ADD => numVar.Value + Value,
                    NumericOp.MULTIPLY => numVar.Value * Value,
                    _ => Value,
                };
            }
            else
            {
                throw new VariableTypeMismatchException();
            }
        }
    }
}
