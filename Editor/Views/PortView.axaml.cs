using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Editor.ViewModels;

namespace Editor.Views;

public partial class PortView : UserControl
{
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