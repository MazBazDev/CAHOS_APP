using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using App.Models;
using App.Services;
using ReactiveUI;

namespace App.ViewModels.Pages;

public partial class StockIndexViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _mainWindowViewModel;
    private readonly ProductService _productService;
    
    public string Title => "Stocks";
    
    private ObservableCollection<Product> _products = new();

    public ObservableCollection<Product> Products
    {
        get => _products;
        set => this.RaiseAndSetIfChanged(ref _products, value);
    }
    
    public StockIndexViewModel(MainWindowViewModel mainWindowViewModel)
    {
        _mainWindowViewModel = mainWindowViewModel;
        _productService = new ProductService();
        Products = new ObservableCollection<Product>();
        LoadProductsAsync();
    }

    public async void LoadProductsAsync()
    {
        try
        {
            Console.WriteLine("Loading products...");
            var response = await _productService.GetProductsAsync();

            if (response.IsSuccess)
            {
                Console.WriteLine("Products loaded successfully!");
                
                Products.Clear();
                foreach (var product in response.Data)
                {
                    Products.Add(product);
                }
            } else {
                _mainWindowViewModel.SetError($"An error has occurred : {response.ApiException?.Message ?? "Erreur inconnue"}", 5000);
            }
        }
        catch (Exception ex) {
            _mainWindowViewModel.SetError($"An error has occurred. Try again later.", 5000);
        }
    }
}
