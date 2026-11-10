using System.ComponentModel;
using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Editor.ViewModels;

public partial class PageNodeViewModel : NodeViewModelBase, IConnectionTarget, IConnectionSource
{
    public PageNodeViewModel(double x, double y) : base(x, y)
    {
        Title = "Page";
        Content = "Start writing your page...";
    }

    [ObservableProperty] public partial Point Incoming { get; set; }
    [ObservableProperty] public partial Point Outgoing { get; set; }
    [ObservableProperty] public partial string Content { get; set; }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (e.PropertyName is nameof(X) or nameof(Y) or nameof(Width) or nameof(Height))
        {
            OnPropertyChanged(nameof(Incoming));   
            OnPropertyChanged(nameof(Outgoing));
        }
    }
}