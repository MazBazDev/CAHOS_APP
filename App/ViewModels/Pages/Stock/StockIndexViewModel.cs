using System;
using System.Collections.ObjectModel;
using App.Models;
using App.Services;
using ReactiveUI;

namespace App.ViewModels.Pages.Stock;

public partial class StockIndexViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _mainWindowViewModel;
    private readonly ProductService _productService;
    
    public string Title => "Stocks";
    
    private ObservableCollection<Product> _products;

    public ObservableCollection<Product> Products
    {
        get => _products;
        set => this.RaiseAndSetIfChanged(ref _products, value);
    }
    
    public StockIndexViewModel(MainWindowViewModel mainWindowViewModel)
    {
        _mainWindowViewModel = mainWindowViewModel;
        _productService = new ProductService();
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
                Products = new ObservableCollection<Product>(response.Data);
            } else {
                _mainWindowViewModel.SetError($"An error has occurred : {response.ApiException?.Message ?? "Erreur inconnue"}", 5000);
            }
        }
        catch (Exception ex) {
            _mainWindowViewModel.SetError($"An error has occurred. Try again later.", 5000);
        }
    }
}
