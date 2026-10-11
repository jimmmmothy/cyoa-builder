using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using Editor.ViewModels;

namespace Editor.Views;

public partial class NodeViewBase : UserControl
{
    private Point previous;

    public NodeViewBase()
    {
        AddHandler(PointerPressedEvent, Node_PointerPressed);
        AddHandler(PointerReleasedEvent, Node_PointerReleased);
        AddHandler(PointerMovedEvent, Node_PointerMoved);
    }

    private void Node_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        Classes.Add("selected");

        if (e.Source is not Ellipse ellipse)
        {
            HandleNodeDrag(e, PointerFunctionStage.Pressed);
            return;
        }

        var port = ellipse.FindAncestorOfType<PortView>();

        if (port == null)
        {
            Console.WriteLine("Could not find a port");
            return;
        }

        switch (port.Direction)
        {
            case PortDirection.Incoming:
                {
                    if (DataContext is IConnectionTarget vm)
                    {
                        vm.HandleTargetClick();
                    }
                }
                break;
            case PortDirection.Outgoing:
                {
                    if (DataContext is IConnectionSource vm)
                    {
                        vm.HandleSourceClick();
                    }
                }
                break;
            default:
                Console.WriteLine("Somehow Palpatine has returned...");
                break;
        }
    }

    private void Node_PointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        HandleNodeDrag(e, PointerFunctionStage.Released);
    }

    private void Node_PointerMoved(object? sender, PointerEventArgs e)
    {
        if (e.Pointer.Captured != this)
            return;

        HandleNodeDrag(e, PointerFunctionStage.Moved);
    }

    private void HandleNodeDrag(PointerEventArgs e, PointerFunctionStage stage)
    {
        switch (stage)
        {
            case PointerFunctionStage.Pressed:
                e.Pointer.Capture(this);
                previous = e.GetPosition(this.GetVisualParent());
                break;
            case PointerFunctionStage.Moved:
                var dx = e.GetPosition(this.GetVisualParent()).X - previous.X;
                var dy = e.GetPosition(this.GetVisualParent()).Y - previous.Y;

                if (DataContext is NodeViewModelBase vm)
                {
                    vm.MoveBy(dx, dy);
                }
                break;
            case PointerFunctionStage.Released:
            default:
                Classes.Remove("selected");
                e.Pointer.Capture(null);
                break;
        }
    }
}