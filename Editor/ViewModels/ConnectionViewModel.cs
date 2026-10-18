using System;
using System.ComponentModel;
using System.Security.Cryptography;
using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Editor.ViewModels;

public partial class ConnectionViewModel : ViewModelBase
{
    [ObservableProperty] 
    [NotifyPropertyChangedFor(nameof(StartPoint), nameof(FirstMidpoint))]
    public partial IConnectionSource? From { get; set; } = null;
    [ObservableProperty] 
    [NotifyPropertyChangedFor(nameof(EndPoint), nameof(SecondMidpoint))]
    public partial IConnectionTarget? To { get; set; } = null;
    public Point StartPoint 
    {
        get
        {
            if (From is not null) 
                return From.Outgoing;
            else return field;
        }
        set
        {
            field = value;
            OnPropertyChanged(nameof(StartPoint));
            OnPropertyChanged(nameof(FirstMidpoint));
        }
    }
    public Point EndPoint 
    {
        get
        {
            if (To is not null)
                return To.Incoming;
            else return field;
        }
        set
        {
            field = value;
            OnPropertyChanged(nameof(EndPoint));
            OnPropertyChanged(nameof(SecondMidpoint));
        }
    }
    public Point FirstMidpoint => new(StartPoint.X + 50, StartPoint.Y);
    public Point SecondMidpoint => new(EndPoint.X - 50, EndPoint.Y);

    partial void OnFromChanged(IConnectionSource? oldValue, IConnectionSource? newValue)
    {
        if (oldValue is not null) oldValue.PropertyChanged -= OnFromPropertyChanged;
        if (newValue is not null) newValue.PropertyChanged += OnFromPropertyChanged;
    }

    partial void OnToChanged(IConnectionTarget? oldValue, IConnectionTarget? newValue)
    {
        if (oldValue is not null) oldValue.PropertyChanged -= OnToPropertyChanged;
        if (newValue is not null) newValue.PropertyChanged += OnToPropertyChanged;
    }

    private void OnFromPropertyChanged(object? source, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(IConnectionSource.Outgoing))
        {
            OnPropertyChanged(nameof(StartPoint));
            OnPropertyChanged(nameof(FirstMidpoint));
        }
    }

    private void OnToPropertyChanged(object? source, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(IConnectionTarget.Incoming))
        {   
            OnPropertyChanged(nameof(EndPoint));
            OnPropertyChanged(nameof(SecondMidpoint));
        }
    }
}
