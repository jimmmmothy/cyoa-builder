using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Editor.ViewModels;

public partial class RouteViewModel : ViewModelBase, IConnectionSource
{
    [ObservableProperty] public partial string Label { get; set; } = "Route";
    [ObservableProperty] public partial Point Outgoing { get; set; }
}