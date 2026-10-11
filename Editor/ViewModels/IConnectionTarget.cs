using System.ComponentModel;
using Avalonia;

namespace Editor.ViewModels;

public partial interface IConnectionTarget : INotifyPropertyChanged
{
    Point Incoming { get; }

    public void HandleTargetClick();
}
