using cyoa_core.src.exceptions;

namespace cyoa_core.src
{
    // Numeric conditions are met when following the order ACTUAL->COMPARATOR->EXPECTED
    // For example: Actual is greater than Expected
    public class NumericCondition : Condition
    {
        public Comparator Op { get; set; }
        public double Expected { get; set; }
        public override bool IsMet(StoryState ss)
        {
            var variable = ss.Variables[Target.Guid] ?? throw new ArgumentNullException();

            if (variable is NumericVariable numVar)
            {
                return Op switch
                {
                    Comparator.EQUALS => numVar.Value == Expected,
                    Comparator.GREATER_THAN => numVar.Value > Expected,
                    Comparator.LESS_THAN => numVar.Value < Expected,
                    Comparator.GREATER_OR_EQUAL => numVar.Value >= Expected,
                    Comparator.LESS_OR_EQUAL => numVar.Value <= Expected,
                    _ => numVar.Value == Expected,
                };
            }
            else
            {
                throw new VariableTypeMismatchException();
            }
        }
    }
}
