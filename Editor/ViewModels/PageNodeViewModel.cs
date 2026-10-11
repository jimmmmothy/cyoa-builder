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

    public Point Incoming => new(X, Y + Height / 2.0);
    public Point Outgoing => new(X + Width, Y + Height / 2.0);
    [ObservableProperty] public partial string Content { get; set; }

    public void HandleTargetClick() // Incoming
    {
        System.Console.WriteLine("Clicked Incoming!!!");
    }

    public void HandleSourceClick() // Outgoing
    {
        System.Console.WriteLine("Clicked Outgoing!!!");
    }

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