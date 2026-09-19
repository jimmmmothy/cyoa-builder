using cyoa_core.src;

namespace cyoa_core_tests;

public class ConditionCheckingTests
{
    private readonly StoryState ss;

    public ConditionCheckingTests()
    {
        ss = TestUtilities.CreateStoryState();
    }

    [Fact]
    public void BooleanConditions()
    {
        var target = TestUtilities.GetVar<BooleanVariable>(ss, "Boolean 0");
        var condition1 = new BooleanCondition { Target = target, Expected = true };
        var condition2 = new BooleanCondition { Target = target, Expected = false };

        bool isMet1 = condition1.IsMet(ss);
        bool isMet2 = condition2.IsMet(ss);

        Assert.True(isMet1);
        Assert.False(isMet2);
    }

    [Theory]
    [InlineData(Comparator.EQUALS, true, false, false)]
    [InlineData(Comparator.GREATER_THAN, false, true, false)]
    [InlineData(Comparator.LESS_THAN, false, false, true)]
    [InlineData(Comparator.GREATER_OR_EQUAL, true, true, false)]
    [InlineData(Comparator.LESS_OR_EQUAL, true, false, true)]
    public void NumericConditions(Comparator comparator, bool expected1, bool expected2, bool expected3)
    {
        var target = TestUtilities.GetVar<NumericVariable>(ss, "Numeric 3");
        var condition1 = new NumericCondition { Target = target, Expected = 3, Op = comparator };
        var condition2 = new NumericCondition { Target = target, Expected = 1, Op = comparator };
        var condition3 = new NumericCondition { Target = target, Expected = 5, Op = comparator };

        switch (comparator)
        {
            case Comparator.EQUALS:
                Assert.Equal(expected1, condition1.IsMet(ss));
                Assert.Equal(expected2, condition2.IsMet(ss));
                Assert.Equal(expected3, condition3.IsMet(ss));
                break;
            case Comparator.GREATER_THAN:
                Assert.Equal(expected1, condition1.IsMet(ss));
                Assert.Equal(expected2, condition2.IsMet(ss));
                Assert.Equal(expected3, condition3.IsMet(ss));
                break;
            case Comparator.LESS_THAN:
                Assert.Equal(expected1, condition1.IsMet(ss));
                Assert.Equal(expected2, condition2.IsMet(ss));
                Assert.Equal(expected3, condition3.IsMet(ss));
                break;
            case Comparator.GREATER_OR_EQUAL:
                Assert.Equal(expected1, condition1.IsMet(ss));
                Assert.Equal(expected2, condition2.IsMet(ss));
                Assert.Equal(expected3, condition3.IsMet(ss));
                break;
            case Comparator.LESS_OR_EQUAL:
                Assert.Equal(expected1, condition1.IsMet(ss));
                Assert.Equal(expected2, condition2.IsMet(ss));
                Assert.Equal(expected3, condition3.IsMet(ss));
                break;
        }
    }
}