using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Editor.Models;

namespace Editor.ViewModels;

public partial class BranchNodeViewModel : NodeViewModelBase
{
    public BranchNodeViewModel(double x, double y) : base(x, y)
    {
        Title = "Branch";
    }

    [ObservableProperty]
    public partial ObservableCollection<Choice> Choices { get; set; } = [];
}
