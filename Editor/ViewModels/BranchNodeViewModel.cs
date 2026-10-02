using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Editor.ViewModels;

public partial class BranchNodeViewModel(int x, int y) : NodeViewModelBase(x, y)
{
    // [ObservableProperty]
    // public partial ObservableCollection<Choice> Choices { get; set; } = [];
}
