using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;
using Editor.Models;

namespace Editor.ViewModels;

public partial class BranchNodeViewModel : NodeViewModelBase, IConnectionTarget
{
    public BranchNodeViewModel(double x, double y) : base(x, y)
    {
        Title = "Branch";
    }

    [ObservableProperty]
    public partial ObservableCollection<Choice> Choices { get; set; } = [];

    public Point Incoming => new(X, Y + Height / 2.0);

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (e.PropertyName is nameof(X) or nameof(Y) or nameof(Width) or nameof(Height))
        {
            OnPropertyChanged(nameof(Incoming));   
        }
    }
}
