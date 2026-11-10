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
        if (DataContext is not NodeViewModelBase vm)
            return;

        switch (stage)
        {
            case PointerFunctionStage.Pressed:
                e.Pointer.Capture(this);
                previous = e.GetPosition(this.GetVisualParent());
                var args = new NodeSelectedEventArgs(NodeSelectedEvent, vm);
                RaiseEvent(args);
                break;
            case PointerFunctionStage.Moved:
                if (e.Pointer.Captured != this)
                    return;
                var dx = e.GetPosition(this.GetVisualParent()).X - previous.X;
                var dy = e.GetPosition(this.GetVisualParent()).Y - previous.Y;

                vm.MoveBy(dx, dy);
                break;
            case PointerFunctionStage.Released:
            default:
                e.Pointer.Capture(null);
                break;
        }
    }

    public static readonly RoutedEvent<NodeSelectedEventArgs> NodeSelectedEvent = 
        RoutedEvent.Register<NodeViewBase, NodeSelectedEventArgs>(
            nameof(NodeSelected), RoutingStrategies.Bubble
        );
    
    public event EventHandler<NodeSelectedEventArgs> NodeSelected
    {
        add => AddHandler(NodeSelectedEvent, value);
        remove => RemoveHandler(NodeSelectedEvent, value);
    }

    public class NodeSelectedEventArgs(RoutedEvent routedEvent, NodeViewModelBase nodeViewModelBase) : RoutedEventArgs (routedEvent)
    {
        public NodeViewModelBase VM { get; } = nodeViewModelBase;
    }
}