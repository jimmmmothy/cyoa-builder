using System;
using System.Collections.ObjectModel;
using System.Linq;
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

    [ObservableProperty] 
    public partial ObservableCollection<NodeViewModelBase> Nodes { get; set; } = [];
    [ObservableProperty] 
    public partial ObservableCollection<ConnectionViewModel> Connections { get; set; } = [];
    [ObservableProperty] 
    public partial ConnectionViewModel? SelectedConnection { get; private set; }
    [ObservableProperty]
    public partial NodeViewModelBase? SelectedNode { get; set; }

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

    public void SelectNode(NodeViewModelBase node)
    {
        SelectedNode?.IsSelected = false;
        SelectedNode = node;
        SelectedNode.IsSelected = true;
    }

    public void AddPendingConnection(IConnectionSource start, Point end)
    {
        SelectedConnection = new ConnectionViewModel
        {
            From = start,
            EndPoint = end
        };
        
        var existing = Connections.FirstOrDefault(c => c.From == start);
        if (existing != null)
        {
            Connections.Remove(existing); // Overwriting
        }
        Connections.Add(SelectedConnection);
    }

    public void AddPendingConnection(Point start, IConnectionTarget end)
    {
        SelectedConnection = new ConnectionViewModel
        {
            StartPoint = start,
            To = end
        };
        Connections.Add(SelectedConnection);
    }

    public void AddPendingConnection(Point start, Point end)
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

    public void FinishConnection(IConnectionSource source)
    {
        if (SelectedConnection is null)
            return;
        if (ValidateConnection(source))
            SelectedConnection.From = source;
        else
            RemovePendingConnection();
    }

    public void FinishConnection(IConnectionTarget target)
    {
        if (SelectedConnection is null)
            return;
        if (ValidateConnection(target))
            SelectedConnection.To = target;
        else
            RemovePendingConnection();
    }

    private bool ValidateConnection(IConnectionSource source) // Incoming port is known, the outgoing (source) port is being verified
    {
        if (SelectedConnection is null || SelectedConnection.To is null)
            return false;

        var existing = Connections.FirstOrDefault(c => c.From == source);
        if (existing != null)
            return false;

        else if (SelectedConnection.To is PageNodeViewModel p && source == p)
            return false;

        else if (SelectedConnection.To is BranchNodeViewModel b && 
                 source is ChoiceViewModel s && 
                 b.Choices.Contains(s))
            return false;

        else if (SelectedConnection.To is ConditionalBranchNodeViewModel c && 
                 source is RouteViewModel s2 && 
                 c.Routes.Contains(s2))
            return false;

        return true;
    }

    private bool ValidateConnection(IConnectionTarget target) // Outgoing port is known, the incoming (target) port is being verified
    {
        if (SelectedConnection is null || SelectedConnection.From is null)
            return false;

        else if (SelectedConnection.From is PageNodeViewModel p && target == p)
            return false;

        else if (SelectedConnection.From is ChoiceViewModel c &&
                 target is BranchNodeViewModel b && 
                 b.Choices.Contains(c))
            return false;

        else if (SelectedConnection.From is ConditionalBranchNodeViewModel cb && target == cb)
            return false;
            
        else if (SelectedConnection.From is RouteViewModel r &&
                 target is ConditionalBranchNodeViewModel cb2 &&
                 cb2.Routes.Contains(r))
            return false;

        return true;
    }
}