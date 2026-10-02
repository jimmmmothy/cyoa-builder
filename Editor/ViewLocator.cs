using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Editor.ViewModels;

namespace Editor;

public class ViewLocator : IDataTemplate
{
    public Control? Build(object? param)
    {
        if (param is null)
            return null;

        var viewName = param.GetType().FullName!.Replace("ViewModel", "View", StringComparison.InvariantCulture);
        var type = Type.GetType(viewName);

        if (type is null)
            return null;

        var control = (Control)Activator.CreateInstance(type)!;
        control.DataContext = param;
        return control;
    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}