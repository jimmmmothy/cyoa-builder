using System;
using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Editor.ViewModels;

public partial class ConnectionViewModel : ViewModelBase
{
    [ObservableProperty] public partial Point StartPoint { get; set; }
    [ObservableProperty] public partial Point EndPoint { get; set; }
}
