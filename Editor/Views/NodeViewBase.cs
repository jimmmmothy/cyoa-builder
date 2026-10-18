using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Interactivity;
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
        HandleNodeDrag(e, PointerFunctionStage.Pressed);
    }

    private void Node_PointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        HandleNodeDrag(e, PointerFunctionStage.Released);
    }

    private void Node_PointerMoved(object? sender, PointerEventArgs e)
    {
        HandleNodeDrag(e, PointerFunctionStage.Moved);
    }

    private void HandleNodeDrag(PointerEventArgs e, PointerFunctionStage stage)
    {
        switch (stage)
        {
            case PointerFunctionStage.Pressed:
                Classes.Add("selected");
                e.Pointer.Capture(this);
                previous = e.GetPosition(this.GetVisualParent());
                break;
            case PointerFunctionStage.Moved:
                if (e.Pointer.Captured != this)
                    return;
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