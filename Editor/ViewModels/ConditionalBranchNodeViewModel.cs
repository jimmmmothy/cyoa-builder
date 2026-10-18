using System.ComponentModel;
using Avalonia;
using Editor.ViewModels;

namespace Editor.ViewModels;

public partial class ConditionalBranchNodeViewModel : NodeViewModelBase, IConnectionTarget
{
    public ConditionalBranchNodeViewModel(double x, double y) : base(x, y)
    {
        Title = "Conditional Branch";        
    }

    public Point Incoming => new(X, Y + Height / 2.0);

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (e.PropertyName is nameof(X) or nameof(Y) or nameof(Width) or nameof(Height))
        {
            OnPropertyChanged(nameof(Incoming));   
        }
    }
}