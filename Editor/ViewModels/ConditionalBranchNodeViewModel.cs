using Editor.ViewModels;

namespace Editor.ViewModels;

public partial class ConditionalBranchNodeViewModel : NodeViewModelBase
{
    public ConditionalBranchNodeViewModel(double x, double y) : base(x, y)
    {
        Title = "Conditional Branch";        
    }
}