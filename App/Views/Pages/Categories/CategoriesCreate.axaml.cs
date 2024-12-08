using System;
using App.Models;
using App.Services;
using App.ViewModels;
using App.ViewModels.Pages.Categories;
using App.ViewModels.Pages.Orders;
using App.Views.Pages.Orders;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;

namespace App.Views.Pages.Categories;

public partial class CategoriesCreate : UserControl
{
    private readonly CategoriesService CategoriesService;
    private readonly MainWindowViewModel _mainWindowViewModel;

    public CategoriesCreate(MainWindowViewModel mainWindowViewModel)
    {
        _mainWindowViewModel = mainWindowViewModel;
        this.DataContext = new CategoriesCreateViewModel(mainWindowViewModel);
        CategoriesService = new CategoriesService();
        InitializeComponent();
    }

    private async void StoreCategory(object? sender, RoutedEventArgs e)
    {
        try
        {
            var createCategoryResult = await CategoriesService.CreateCategoryAsync(
                new Category {
                    name = NameInput.Text,
                }
            );
            
            if (createCategoryResult.IsSuccess && createCategoryResult.Data != null)
            {
                _mainWindowViewModel.SetSuccess("Category created successfully!", 3000);
                var mainWindow = (MainWindow)this.FindAncestorOfType<Window>();
                mainWindow?.NavigateToPage(new CategoriesIndex(_mainWindowViewModel));
            }
            else
            {
                var errorMessage = createCategoryResult.ApiException?.Message 
                                   ?? createCategoryResult.GeneralException?.Message 
                                   ?? "An unexpected error occurred!";
                _mainWindowViewModel.SetError(errorMessage, 5000);
            }
        }
        catch (Exception ex)
        {
            _mainWindowViewModel.SetError($"Error : {ex.Message}", 5000);
        }
    }
}
