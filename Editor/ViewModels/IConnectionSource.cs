using System.ComponentModel;
using Avalonia;

namespace Editor.ViewModels;

public partial interface IConnectionSource : INotifyPropertyChanged
{
    Point Outgoing { get; }

    public void HandleSourceClick();
}
