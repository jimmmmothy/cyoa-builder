using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Editor.ViewModels;

namespace Editor.Views;

public partial class RoutePropertiesView : UserControl
{
    public RoutePropertiesView()
    {
        InitializeComponent();
    }

    public static readonly RoutedEvent<RouteDeletedEventArgs> RouteDeletedEvent =
        RoutedEvent.Register<RoutePropertiesView, RouteDeletedEventArgs>(
            nameof(RouteDeleted), RoutingStrategies.Bubble
        );

    public event EventHandler<RouteDeletedEventArgs> RouteDeleted
    {
        add => AddHandler(RouteDeletedEvent, value);
        remove => RemoveHandler(RouteDeletedEvent, value);
    }

    public class RouteDeletedEventArgs(RoutedEvent routedEvent, RouteViewModel vm) : RoutedEventArgs(routedEvent)
    {
        public RouteViewModel VM { get; } = vm;
    }

    private void Button_DeleteClick(object? sender, RoutedEventArgs e)
    {
        var vm = DataContext as RouteViewModel;
        var args = new RouteDeletedEventArgs(RouteDeletedEvent, vm!);
        RaiseEvent(args);
        e.Handled = true;
    }
}