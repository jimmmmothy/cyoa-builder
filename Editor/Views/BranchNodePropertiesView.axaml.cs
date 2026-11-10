using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Editor.ViewModels;

namespace Editor.Views;

public partial class BranchNodePropertiesView : UserControl
{
    public BranchNodePropertiesView()
    {
        InitializeComponent();
        AddHandler(ChoicePropertiesView.ChoiceDeletedEvent, DeleteChoice);
    }

    private void DeleteChoice(object? source, ChoicePropertiesView.ChoiceDeletedEventArgs args)
    {
        var vm = DataContext as BranchNodeViewModel;

        vm!.Choices.Remove(args.VM);
    }
}