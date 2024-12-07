using System;
using System.Threading.Tasks;
using App.Models;
using App.Services;
using App.ViewModels;
using App.ViewModels.Pages.Stock;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Threading;
using Avalonia.VisualTree;

namespace App.Views.Pages.Stocks;

public partial class StockShow : UserControl
{
    private readonly MainWindowViewModel _mainWindowViewModel;
    private readonly ProductService _ProductService;
    private readonly Product _originalProduct;

    public StockShow(MainWindowViewModel mainWindowViewModel, Product product)
    {
        _mainWindowViewModel = mainWindowViewModel;
        this.DataContext = new StockShowViewModel(mainWindowViewModel, product.id);
        _ProductService = new ProductService();
        _originalProduct = product;
        
        InitializeComponent();
    }

    private void OnTextInput(object? sender, TextInputEventArgs e)
    {
        // Autorise uniquement les chiffres
        if (!int.TryParse(e.Text, out _))
        {
            e.Handled = true;
        }
    }

    private async void DeleteProduct(object? sender, RoutedEventArgs e)
    {
        var deleteProductResult = await _ProductService.DeleteProductAsync(_originalProduct.id);
        
        if (deleteProductResult.IsSuccess)
        {
            _mainWindowViewModel.SetSuccess("Product deleted successfully!", 3000);
        
            // Naviguer vers l'index des produits
            var mainWindow = (MainWindow)this.FindAncestorOfType<Window>();
            mainWindow?.NavigateToPage(new StocksIndex(_mainWindowViewModel));
        }
        else
        {
            var errorMessage = deleteProductResult.ApiException?.Message 
                               ?? deleteProductResult.GeneralException?.Message 
                               ?? "An unexpected error occurred!";
            _mainWindowViewModel.SetError(errorMessage, 5000);
        }
    }

    private async void UpdateProduct(object? sender, RoutedEventArgs e)
{
    try
    {
        var updateProduct = new Product();
        
        if (QuantityInput.Value.HasValue && QuantityInput.Value.Value != _originalProduct.quantity)
        {
            updateProduct.quantity = (int)QuantityInput.Value.Value;
        }

        if (PriceInput.Value.HasValue && PriceInput.Value.Value != _originalProduct.price)
        {
            updateProduct.price = PriceInput.Value.Value;
        }

        if (!string.IsNullOrWhiteSpace(NameInput.Text) && NameInput.Text != _originalProduct.name)
        {
            updateProduct.name = NameInput.Text;
        }
        
        if (EXPDateInput.SelectedDate.HasValue && EXPDateInput.SelectedDate.Value.ToString("yyyy-MM-dd") != _originalProduct.expiration_date.ToString("yyyy-MM-dd"))
        {
            updateProduct.expiration_date = EXPDateInput.SelectedDate.Value.Date;
        }

        if (!string.IsNullOrWhiteSpace(LocationInput.Text) && LocationInput.Text != _originalProduct.location)
        {
            updateProduct.location = LocationInput.Text;
        }

        var selectedCategory = CategoryInput.SelectedItem as Category;
        if (selectedCategory != null && selectedCategory.id != _originalProduct.category_id)
        {
            updateProduct.category_id = selectedCategory.id;
        }
        
        if (updateProduct.Equals(new Product())) {
            _mainWindowViewModel.SetError("No changes were made.", 3000);
            return;
        }
        
        var updateProductResult = await _ProductService.UpdateProductAsync(_originalProduct.id, updateProduct);
        
        if (updateProductResult.IsSuccess && updateProductResult.Data != null)
        {
            _mainWindowViewModel.SetSuccess("Product updated successfully!", 3000);
        
            // Naviguer vers l'index des produits
            var mainWindow = (MainWindow)this.FindAncestorOfType<Window>();
            mainWindow?.NavigateToPage(new StocksIndex(_mainWindowViewModel));
        }
        else
        {
            var errorMessage = updateProductResult.ApiException?.Message 
                               ?? updateProductResult.GeneralException?.Message 
                               ?? "An unexpected error occurred!";
            _mainWindowViewModel.SetError(errorMessage, 5000);
        }
    }
    catch (Exception ex)
    {
        _mainWindowViewModel.SetError($"Error: {ex.Message}", 5000);
    }
}

}