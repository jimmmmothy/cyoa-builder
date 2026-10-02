using CommunityToolkit.Mvvm.ComponentModel;

namespace Editor.ViewModels;

public partial class PageNodeViewModel(int x, int y) : NodeViewModelBase(x, y)
{
    [ObservableProperty]
    public partial string Content { get; set; } = "Page";
}