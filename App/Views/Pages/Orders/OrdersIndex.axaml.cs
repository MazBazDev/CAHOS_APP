using App.Interfaces;
using App.Models;
using App.ViewModels;
using App.ViewModels.Pages.Orders;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;

namespace App.Views.Pages.Orders;

public partial class OrdersIndex : UserControl, IRefreshable
{
    private readonly MainWindowViewModel _mainWindowViewModel;
    
    public OrdersIndex(MainWindowViewModel mainWindowViewModel)
    {
        _mainWindowViewModel = mainWindowViewModel;
        this.DataContext = new OrderIndexViewModel(mainWindowViewModel);
        InitializeComponent();
    }

    private void AddOrderButton(object? sender, RoutedEventArgs routedEventArgs)
    {
        var mainWindow = (MainWindow)this.FindAncestorOfType<Window>();
        mainWindow?.NavigateToPage(new Orders.OrdersCreate(_mainWindowViewModel));
    }

    private void ShowOrder(object? sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count == 0) return;
        var order = (Order)e.AddedItems[0];
        var mainWindow = (MainWindow)this.FindAncestorOfType<Window>();
        mainWindow?.NavigateToPage(new OrderShow(_mainWindowViewModel, order));
    }

    public void Refresh()
    {
        this.DataContext = new OrderIndexViewModel(_mainWindowViewModel);
    }
}