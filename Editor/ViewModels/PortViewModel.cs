using System;
using CommunityToolkit.Mvvm.Input;

namespace Editor.ViewModels;

public partial class PortViewModel : ViewModelBase
{
    public void LogHit()
    {
        Console.WriteLine("HITTY!");
    }
}
