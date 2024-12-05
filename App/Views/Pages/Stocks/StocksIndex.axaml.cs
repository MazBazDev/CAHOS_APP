using App.ViewModels;
using App.ViewModels.Pages;
using App.Views.Pages.Orders;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;

namespace App.Views.Pages.Stocks;

public partial class StocksIndex : UserControl
{
    public StocksIndex(MainWindowViewModel mainWindowViewModel)
    {
        this.DataContext = new StockIndexViewModel(mainWindowViewModel);
        InitializeComponent();
    }

    private void AddProductButton(object? sender, RoutedEventArgs routedEventArgs)
    {
        var mainWindow = (MainWindow)this.FindAncestorOfType<Window>();
        mainWindow?.NavigateToPage(new StocksCreate());
    }
}