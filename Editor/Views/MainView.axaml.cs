using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.LogicalTree;
using Avalonia.VisualTree;
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
        AddHandler(NodeViewBase.NodeSelectedEvent, Node_Selected);
    }

    private PortDirection _latestDirection;

    private void Node_Selected(object? source, NodeViewBase.NodeSelectedEventArgs args)
    {
        var mainVM = DataContext as MainViewModel;
        mainVM!.SelectNode(args.VM);
    }

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
                    mainVM!.AddPendingConnection(args.PointerEvent.GetPosition(Workspace), portVM!);
                    break;
                }
            case PortDirection.Outgoing:
                {
                    var portVM = args.VM as IConnectionSource;
                    _latestDirection = PortDirection.Outgoing;
                    mainVM!.AddPendingConnection(portVM!, args.PointerEvent.GetPosition(Workspace));
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
        
        switch (_latestDirection)
        {
            case PortDirection.Incoming:    // Clicked on an Incoming port, so looking for an IConnectionSource
                {
                    var temp = FindAncestorWithContext<IConnectionSource>(hit as Visual);

                    if (temp is not null)
                    {
                        mainVM!.FinishConnection((IConnectionSource)temp.DataContext!);
                    }
                    else
                        mainVM!.RemovePendingConnection();
                }
                break;
            case PortDirection.Outgoing:    // Clicked on an Outgoing port, so looking for an IConnectionTarget
                {
                    var temp = FindAncestorWithContext<IConnectionTarget>(hit as Visual);

                    if (temp is not null)
                    {
                        mainVM!.FinishConnection((IConnectionTarget)temp.DataContext!);
                    }
                    else
                        mainVM!.RemovePendingConnection();
                }
                break;
            default:
                Console.WriteLine("Unknown PortDirection");
                break;
        }

        e.Pointer.Capture(null);
    }

    private Visual? FindAncestorWithContext<T>(Visual? element)
    {
        bool found = false;

        while (!found && element is not null)
        {
            if (element!.DataContext is T)
            {
                found = true;
            }
            else
            {
                element = element.GetVisualParent();
            }
        }

        return found ? element : null;
    }
}