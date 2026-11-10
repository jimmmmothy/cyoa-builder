using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Editor.ViewModels;

namespace Editor.Views;

public partial class ConditionalBranchNodePropertiesView : UserControl
{
    public ConditionalBranchNodePropertiesView()
    {
        InitializeComponent();
        AddHandler(RoutePropertiesView.RouteDeletedEvent, DeleteRoute);
    }

    private void DeleteRoute(object? source, RoutePropertiesView.RouteDeletedEventArgs args)
    {
        var vm = DataContext as ConditionalBranchNodeViewModel;

        vm!.Routes.Remove(args.VM);
    }
}