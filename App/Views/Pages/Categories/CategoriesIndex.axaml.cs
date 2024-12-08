using App.Interfaces;
using App.Models;
using App.ViewModels;
using App.ViewModels.Pages.Categories;
using App.ViewModels.Pages.Orders;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;

namespace App.Views.Pages.Categories;

public partial class CategoriesIndex : UserControl, IRefreshable
{
    private readonly MainWindowViewModel _mainWindowViewModel;
    
    public CategoriesIndex(MainWindowViewModel mainWindowViewModel)
    {
        _mainWindowViewModel = mainWindowViewModel;
        this.DataContext = new CategoriesIndexViewModel(mainWindowViewModel);
        InitializeComponent();
    }

    private void AddCategoryButton(object? sender, RoutedEventArgs routedEventArgs)
    {
        var mainWindow = (MainWindow)this.FindAncestorOfType<Window>();
        mainWindow?.NavigateToPage(new CategoriesCreate(_mainWindowViewModel));
    }

    private void ShowCategory(object? sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count == 0) return;
        var category = (Category)e.AddedItems[0];
        var mainWindow = (MainWindow)this.FindAncestorOfType<Window>();
        mainWindow?.NavigateToPage(new CategoriesShow(_mainWindowViewModel, category));
    }

    public void Refresh()
    {
        this.DataContext = new CategoriesIndexViewModel(_mainWindowViewModel);
    }
}