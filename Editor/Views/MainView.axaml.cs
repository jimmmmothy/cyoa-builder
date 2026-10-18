using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using CommunityToolkit.Mvvm.ComponentModel;
using Editor.Models;
using Editor.ViewModels;

namespace Editor.Views;

public partial class MainView : Window
{
    public MainView()
    {
        InitializeComponent();
        AddHandler(PortView.ConnectionDragStartedEvent, Connection_DragStarted);
        AddHandler(PointerMovedEvent, Connection_DragMoved);
        AddHandler(PointerReleasedEvent, Connection_DragReleased);
    }

    private PortDirection _latestDirection;

    private void Connection_DragStarted(object? source, PortView.ConnectionDragStartedArgs args)
    {
        var mainVM = DataContext as MainViewModel;
        args.PointerEvent.Pointer.Capture(this);

        switch (args.Direction)
        {
            case PortDirection.Incoming:
                {
                    var portVM = args.VM as IConnectionTarget;
                    _latestDirection = PortDirection.Incoming;
                    mainVM!.AddConnection(args.PointerEvent.GetPosition(Workspace), portVM!);
                    break;
                }
            case PortDirection.Outgoing:
                {
                    var portVM = args.VM as IConnectionSource;
                    _latestDirection = PortDirection.Outgoing;
                    mainVM!.AddConnection(portVM!, args.PointerEvent.GetPosition(Workspace));
                    break;
                }
            default:
                Console.WriteLine("Unknown PortDirection");
                break;
        }
    }

    private void Connection_DragMoved(object? source, PointerEventArgs e)
    {
        if (e.Pointer.Captured != this)
            return;

        var mainVM = DataContext as MainViewModel;
        switch (_latestDirection)
        {
            case PortDirection.Incoming:
                mainVM!.SelectedConnection!.StartPoint = e.GetPosition(Workspace);
                break;
            case PortDirection.Outgoing:
                mainVM!.SelectedConnection!.EndPoint = e.GetPosition(Workspace);
                break;
            default:
                Console.WriteLine("Unknown PortDirection");
                break;
        }
    }

    private void Connection_DragReleased(object? source, PointerReleasedEventArgs e)
    {
        if (e.Pointer.Captured != this)
            return;

        var mainVM = DataContext as MainViewModel;
        var hit = Workspace.InputHitTest(e.GetPosition(Workspace));
        var node = (hit as Visual).FindAncestorOfType<NodeViewBase>(includeSelf: true);
            
        if (node is null)
        {
            mainVM!.RemovePendingConnection();
            e.Pointer.Capture(null);
            return;
        }

        switch (_latestDirection)
        {
            case PortDirection.Incoming:
                mainVM!.SelectedConnection!.From = (IConnectionSource)node.DataContext!;
                break;
            case PortDirection.Outgoing:
                mainVM!.SelectedConnection!.To = (IConnectionTarget)node.DataContext!;
                break;
            default:
                Console.WriteLine("Unknown PortDirection");
                break;
        }

        e.Pointer.Capture(null);
    }
}