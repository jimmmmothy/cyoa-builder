using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using Editor.ViewModels;

namespace Editor.Views;

public partial class PortView : UserControl
{
    private Canvas? _canvasBase;
    public PortView()
    {
        InitializeComponent();
        AddHandler(PointerPressedEvent, OnConnectionDragStarted);
    }

    public static readonly StyledProperty<PortDirection> DirectionProperty =
        AvaloniaProperty.Register<PortView, PortDirection>(nameof(Direction));

    public PortDirection Direction
    {
        get => GetValue(DirectionProperty);
        set => SetValue(DirectionProperty, value);
    }

    public static readonly StyledProperty<Point> AnchorProperty = 
        AvaloniaProperty.Register<PortView, Point>(nameof(Anchor));

    public Point Anchor
    {
        get => GetValue(AnchorProperty);
        set => SetValue(AnchorProperty, value);
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        _canvasBase = this.FindAncestorOfType<Canvas>()!;
        LayoutUpdated += UpdateAnchor;
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);
        LayoutUpdated -= UpdateAnchor;
    }

    private void UpdateAnchor(object? sender, EventArgs e)
    {
        var p = this.TranslatePoint(new Point(Bounds.Width / 2, Bounds.Height / 2), _canvasBase!);
        if (p.HasValue)
            Anchor = p.Value;
    }

    // Event stuff
    public static readonly RoutedEvent<ConnectionDragStartedArgs> ConnectionDragStartedEvent =
        RoutedEvent.Register<PortView, ConnectionDragStartedArgs>(
            nameof(ConnectionDragStarted), RoutingStrategies.Bubble
        );

    public event EventHandler<ConnectionDragStartedArgs> ConnectionDragStarted
    {
        add => AddHandler(ConnectionDragStartedEvent, value);
        remove => RemoveHandler(ConnectionDragStartedEvent, value);
    }

    public void OnConnectionDragStarted(object? sender, PointerPressedEventArgs e)
    {
        if ((Direction == PortDirection.Incoming && DataContext is not IConnectionTarget) ||
            (Direction == PortDirection.Outgoing && DataContext is not IConnectionSource))
        {
            Console.WriteLine("Mismatch between Port direction and view model identity");
            e.Handled = true;
            return;
        }

        var args = new ConnectionDragStartedArgs(ConnectionDragStartedEvent, Direction, (ViewModelBase)DataContext!, e);
        RaiseEvent(args);
        e.Handled = true;
    }

    public class ConnectionDragStartedArgs(RoutedEvent routedEvent, PortDirection direction, ViewModelBase vm, PointerPressedEventArgs e) : RoutedEventArgs(routedEvent)
    {
        public PortDirection Direction { get; } = direction;
        public ViewModelBase VM { get; } = vm;
        public PointerPressedEventArgs PointerEvent { get; } = e;
    }
}