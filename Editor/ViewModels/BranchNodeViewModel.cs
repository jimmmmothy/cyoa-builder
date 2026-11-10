using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Editor.ViewModels;

public partial class BranchNodeViewModel : NodeViewModelBase, IConnectionTarget
{
    public BranchNodeViewModel(double x, double y) : base(x, y)
    {
        Title = "Branch";
        Choices.Add(new ChoiceViewModel());
        Choices.Add(new ChoiceViewModel());
        Choices.Add(new ChoiceViewModel());
        Choices.Add(new ChoiceViewModel());
    }

    [ObservableProperty]
    public partial ObservableCollection<ChoiceViewModel> Choices { get; set; } = [];

    [ObservableProperty] public partial Point Incoming {get; set;}

    [RelayCommand]
    public void AddChoice()
    {
        Choices.Add(new ChoiceViewModel());
    }
}
