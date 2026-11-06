using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Editor.ViewModels;

public partial class ChoiceViewModel : ViewModelBase, IConnectionSource
{
    [ObservableProperty] public partial string Label { get; set; } = "Choice";
    [ObservableProperty] public partial Point Outgoing { get; set; }
}