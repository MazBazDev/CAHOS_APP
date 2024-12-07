using System;
using App.Models;
using App.Services;
using App.ViewModels;
using App.ViewModels.Pages;
using App.ViewModels.Pages.Stock;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;

namespace App.Views.Pages.Stocks;

public partial class StocksCreate : UserControl
{
    private readonly ProductService _ProductService;
    private readonly MainWindowViewModel _mainWindowViewModel;

    public StocksCreate(MainWindowViewModel mainWindowViewModel)
    {
        _mainWindowViewModel = mainWindowViewModel;
        this.DataContext = new StockCreateViewModel(mainWindowViewModel);
        _ProductService = new ProductService();
        InitializeComponent();
    }

    private async void StoreProduct(object? sender, RoutedEventArgs e)
    {
        try
        {
            int quantity = QuantityInput.Value.HasValue ? (int)QuantityInput.Value.Value : 0;
            
            decimal price = PriceInput.Value.HasValue ? PriceInput.Value.Value : 0;
            
            var selectedCategory = CategoryInput.SelectedItem as Category;

            if (selectedCategory == null)
            {
                _mainWindowViewModel.SetError("Invalid category, please select one.", 5000);
                return;
            }
        
            
            var createProductResult = await _ProductService.CreateProductAsync(
                new Product
                {
                    name = NameInput.Text,
                    quantity = quantity,
                    price = price,
                    expiration_date = EXPDateInput.SelectedDate.Value.Date,
                    location = LocationInput.Text,
                    category_id = selectedCategory.id
                }
            );
            
            if (createProductResult.IsSuccess && createProductResult.Data != null)
            {
                _mainWindowViewModel.SetSuccess("Product created successfully!", 3000);
                var mainWindow = (MainWindow)this.FindAncestorOfType<Window>();
                mainWindow?.NavigateToPage(new StocksIndex(_mainWindowViewModel));
            }
            else
            {
                var errorMessage = createProductResult.ApiException?.Message 
                                   ?? createProductResult.GeneralException?.Message 
                                   ?? "An unexpected error occurred!";
                _mainWindowViewModel.SetError(errorMessage, 5000);
            }
        }
        catch (Exception ex)
        {
            _mainWindowViewModel.SetError($"Error : {ex.Message}", 5000);
        }
    }

    private void OnTextInput(object? sender, TextInputEventArgs e)
    {
        // Autorise uniquement les chiffres
        if (!int.TryParse(e.Text, out _))
        {
            e.Handled = true;
        }
    }
}
