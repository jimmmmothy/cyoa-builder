using System.Collections.ObjectModel;
using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Editor.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private double _x;
    private double _y;

    public MainViewModel()
    {
        _x = 10.0;
        _y = 10.0;
    }

    [ObservableProperty] public partial ObservableCollection<NodeViewModelBase> Nodes { get; set; } = [];
    [ObservableProperty] public partial ObservableCollection<ConnectionViewModel> Connections { get; set; } = [];
    [ObservableProperty] public partial ConnectionViewModel? SelectedConnection { get; private set; }

    private void SetNextCoords()
    {
        _x += 10;
        _y += 10;

        if (_x > 150 && _y > 150)
        {
            _x = 10;
            _y = 10;
        }
    }

    [RelayCommand]
    private void SpawnPageNode()
    {
        Nodes.Add(new PageNodeViewModel(_x, _y));
        SetNextCoords();
    }

    [RelayCommand]
    private void SpawnBranchNode()
    {
        Nodes.Add(new BranchNodeViewModel(_x, _y));
        SetNextCoords();
    }

    [RelayCommand]
    private void SpawnConditionalBranchNode()
    {
        Nodes.Add(new ConditionalBranchNodeViewModel(_x, _y));
        SetNextCoords();
    }

    public void AddConnection(IConnectionSource start, Point end)
    {
        SelectedConnection = new ConnectionViewModel
        {
            From = start,
            EndPoint = end
        };
        Connections.Add(SelectedConnection);
    }

    public void AddConnection(Point start, IConnectionTarget end)
    {
        SelectedConnection = new ConnectionViewModel
        {
            StartPoint = start,
            To = end
        };
        Connections.Add(SelectedConnection);
    }

    public void AddConnection(Point start, Point end)
    {
        SelectedConnection = new ConnectionViewModel
        {
            StartPoint = start,
            EndPoint = end
        };
        Connections.Add(SelectedConnection);
    }

    public void RemovePendingConnection()
    {
        if (SelectedConnection is not null)
            Connections.Remove(SelectedConnection);
    }
}