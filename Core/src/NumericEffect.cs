using cyoa_core.src.exceptions;

namespace cyoa_core.src
{
    public class NumericEffect : Effect
    {
        public NumericOp Op { get; set; }
        public double Value { get; set; }

        // Think about whether, in the case of non-existing variable, it would be better to
        // add it to the SS dict, or just clone SD variable declarations into SS from the beginning?
        public override void Apply(StoryState ss)
        {
            var variable = ss.Variables[Target.Guid] ?? throw new ArgumentNullException();

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
