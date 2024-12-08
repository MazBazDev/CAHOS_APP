using System;
using App.Models;
using App.Services;
using App.ViewModels;
using App.ViewModels.Pages.Categories;
using App.Views.Pages.Stocks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;

namespace App.Views.Pages.Categories;

public partial class CategoriesShow : UserControl
{
    private readonly MainWindowViewModel _mainWindowViewModel;
    private readonly CategoriesService CategoriesService;
    private readonly Category _originalCategory;

    public CategoriesShow(MainWindowViewModel mainWindowViewModel, Category category)
    {
        _mainWindowViewModel = mainWindowViewModel;
        this.DataContext = new CategoriesShowViewModel(mainWindowViewModel, category);
        CategoriesService = new CategoriesService();
        _originalCategory = category;
        
        InitializeComponent();
    }
    
    private async void DeleteCategory(object? sender, RoutedEventArgs e)
    {
        var deleteProductResult = await CategoriesService.DeleteCategoryAsync(_originalCategory.id);
        
        if (deleteProductResult.IsSuccess)
        {
            _mainWindowViewModel.SetSuccess("Category deleted successfully!", 3000);
            
            var mainWindow = (MainWindow)this.FindAncestorOfType<Window>();
            mainWindow?.NavigateToPage(new CategoriesIndex(_mainWindowViewModel));
        }
        else
        {
            var errorMessage = deleteProductResult.ApiException?.Message 
                               ?? deleteProductResult.GeneralException?.Message 
                               ?? "An unexpected error occurred!";
            _mainWindowViewModel.SetError(errorMessage, 5000);
        }
    }

    private async void UpdateCategory(object? sender, RoutedEventArgs e)
    {
        try {
            var updateCategory = new Category();

            if ( NameInput.Text != _originalCategory.name)
            {
                updateCategory.name = NameInput.Text;
            }
           
            var updateCategoryResult = await CategoriesService.UpdateCategoryAsync(_originalCategory.id, updateCategory);
            
            if (updateCategoryResult.IsSuccess && updateCategoryResult.Data != null)
            {
                _mainWindowViewModel.SetSuccess("Category updated successfully!", 3000);
                
                var mainWindow = (MainWindow)this.FindAncestorOfType<Window>();
                mainWindow?.NavigateToPage(new CategoriesIndex(_mainWindowViewModel));
            }
            else
            {
                var errorMessage = updateCategoryResult.ApiException?.Message 
                                   ?? updateCategoryResult.GeneralException?.Message 
                                   ?? "An unexpected error occurred!";
                _mainWindowViewModel.SetError(errorMessage, 5000);
            }
        }
        catch (Exception ex)
        {
            _mainWindowViewModel.SetError($"Error: {ex.Message}", 5000);
        }
    }

    private void ShowProduct(object? sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count == 0) return;
        var product = (Product)e.AddedItems[0];
        var mainWindow = (MainWindow)this.FindAncestorOfType<Window>();
        mainWindow?.NavigateToPage(new StockShow(_mainWindowViewModel, product));
    }
}