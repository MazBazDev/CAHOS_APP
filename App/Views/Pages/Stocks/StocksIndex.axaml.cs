using App.Interfaces;
using App.Models;
using App.ViewModels;
using App.ViewModels.Pages.Stock;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;

namespace App.Views.Pages.Stocks;

public partial class StocksIndex : UserControl, IRefreshable
{
    private readonly MainWindowViewModel _mainWindowViewModel;
    
    public StocksIndex(MainWindowViewModel mainWindowViewModel)
    {
        _mainWindowViewModel = mainWindowViewModel;
        this.DataContext = new StockIndexViewModel(mainWindowViewModel);
        InitializeComponent();
    }

    private void AddProductButton(object? sender, RoutedEventArgs routedEventArgs)
    {
        var mainWindow = (MainWindow)this.FindAncestorOfType<Window>();
        mainWindow?.NavigateToPage(new StocksCreate(_mainWindowViewModel));
    }

    private void ShowProduct(object? sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count == 0) return;
        var product = (Product)e.AddedItems[0];
        var mainWindow = (MainWindow)this.FindAncestorOfType<Window>();
        mainWindow?.NavigateToPage(new StockShow(_mainWindowViewModel, product));
    }

    public void Refresh()
    {
        this.DataContext = new StockIndexViewModel(_mainWindowViewModel);
    }
}