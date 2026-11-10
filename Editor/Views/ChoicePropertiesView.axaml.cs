using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Editor.ViewModels;

namespace Editor.Views;

public partial class ChoicePropertiesView : UserControl
{
    public ChoicePropertiesView()
    {
        InitializeComponent();
    }


    public static readonly RoutedEvent<ChoiceDeletedEventArgs> ChoiceDeletedEvent =
        RoutedEvent.Register<ChoicePropertiesView, ChoiceDeletedEventArgs>(
            nameof(ChoiceDeleted), RoutingStrategies.Bubble
        );

    public event EventHandler<ChoiceDeletedEventArgs> ChoiceDeleted
    {
        add => AddHandler(ChoiceDeletedEvent, value);
        remove => RemoveHandler(ChoiceDeletedEvent, value);
    }

    public class ChoiceDeletedEventArgs(RoutedEvent routedEvent, ChoiceViewModel vm) : RoutedEventArgs(routedEvent)
    {
        public ChoiceViewModel VM { get; } = vm;
    }

    private void Button_DeleteClick(object? sender, RoutedEventArgs e)
    {
        var vm = DataContext as ChoiceViewModel;
        var args = new ChoiceDeletedEventArgs(ChoiceDeletedEvent, vm!);
        RaiseEvent(args);
        e.Handled = true;
    }
}