using CommunityToolkit.Mvvm.ComponentModel;

namespace Editor.ViewModels;

public partial class NodeViewModelBase(double x, double y) : ViewModelBase
{
    [ObservableProperty] public partial double X { get; set; } = x;
    [ObservableProperty] public partial double Y { get; set; } = y;
    [ObservableProperty] public partial int Width { get; set; } = 300;
    [ObservableProperty] public partial int Height { get; set; } = 100;
    [ObservableProperty] public partial string Title { get; set; }
    
    public void MoveBy(double dx, double dy)
    {
        X += dx;
        Y += dy;
    }
}