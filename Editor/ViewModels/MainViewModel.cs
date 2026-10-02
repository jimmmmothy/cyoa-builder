using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Editor.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private int _x;
    private int _y;

    public MainViewModel()
    {
        _x = 10;
        _y = 10;
    }

    [ObservableProperty]
    public partial ObservableCollection<NodeViewModelBase> Nodes { get; set; } = [];

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
}