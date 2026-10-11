using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Editor.ViewModels;

namespace Editor.Views;

public partial class PortView : UserControl
{
    public PortView()
    {
        InitializeComponent();
        DataContext = new PortViewModel();
    }

    public static readonly StyledProperty<PortDirection> DirectionProperty = 
        AvaloniaProperty.Register<PortView, PortDirection>(nameof(Direction));

    public PortDirection Direction
    {
        get => GetValue(DirectionProperty);
        set => SetValue(DirectionProperty, value);
    }
}