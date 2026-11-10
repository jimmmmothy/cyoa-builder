using System.Collections.ObjectModel;
using System.ComponentModel;
using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Editor.ViewModels;

namespace Editor.ViewModels;

public partial class ConditionalBranchNodeViewModel : NodeViewModelBase, IConnectionTarget, IConnectionSource
{
    public ConditionalBranchNodeViewModel(double x, double y) : base(x, y)
    {
        Title = "Conditional Branch";      
        Routes.Add(new RouteViewModel());  
        Routes.Add(new RouteViewModel());  
    }

    [ObservableProperty]
    public partial ObservableCollection<RouteViewModel> Routes { get; set; } = [];

    [ObservableProperty] public partial Point Incoming { get; set; }

    [ObservableProperty] public partial Point Outgoing { get; set; }

    [RelayCommand]
    public void AddRoute()
    {
        Routes.Add(new RouteViewModel());
    }
}