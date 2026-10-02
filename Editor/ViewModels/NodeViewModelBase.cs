using CommunityToolkit.Mvvm.ComponentModel;

namespace Editor.ViewModels;

public partial class NodeViewModelBase(int x, int y) : ViewModelBase
{
    [ObservableProperty]
    public partial int X { get; set; } = x;
    [ObservableProperty]
    public partial int Y { get; set; } = y;
}