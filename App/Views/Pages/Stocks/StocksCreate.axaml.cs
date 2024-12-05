using App.ViewModels;
using App.ViewModels.Pages;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;

namespace App.Views.Pages.Stocks;

public partial class StocksCreate : UserControl
{
    public StocksCreate()
    {
        this.DataContext = new StockCreateViewModel();
        InitializeComponent();
    }

    private void StoreProduct(object? sender, RoutedEventArgs e)
    {
    }
}