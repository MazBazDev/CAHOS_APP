using System;
using System.Collections.ObjectModel;
using App.Models;
using App.Services;
using ReactiveUI;

namespace App.ViewModels.Pages.Stock;

public class StockShowViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _mainWindowViewModel;
    private readonly CategoriesService _categoriesService;
    private readonly ProductService _ProductService;
    
    
    private ObservableCollection<Category> _categories = new();
    private Product _product = new();
    private string _title = "Product Edit";
    private string _selectedCate = "Select a category";
    
    public string Title
    {
        get  => _title;
        set => this.RaiseAndSetIfChanged(ref _title, value);
    }

    public ObservableCollection<Category> Categories
    {
        get => _categories;
        set => this.RaiseAndSetIfChanged(ref _categories, value);
    }
    
    public Product Product
    {
        get => _product;
        set => this.RaiseAndSetIfChanged(ref _product, value);
    }

    public string SelectedCate {
        get => _selectedCate;
        set => this.RaiseAndSetIfChanged(ref _selectedCate, value);
    }


    public StockShowViewModel(MainWindowViewModel? mainWindowViewModel, int productId)
    {
        _mainWindowViewModel = mainWindowViewModel;
        _categoriesService = new CategoriesService();
        _ProductService = new ProductService();
        
        Categories = new ObservableCollection<Category>();
        Product = new Product();
        
        LoadCategoriesAsync();
        LoadProduct(productId);
    }
    
    public async void LoadCategoriesAsync()
    {
        try
        {
            Console.WriteLine("Loading Categories...");
            var response = await _categoriesService.GetCategoriesAsync();

            if (response.IsSuccess)
            {
                Categories.Clear();
                foreach (var category in response.Data)
                {
                    Categories.Add(category);
                }
            }
            else
            {
                _mainWindowViewModel.SetError($"An error has occurred: {response.ApiException?.Message ?? "Unknown error"}", 5000);
            }
        }
        catch (Exception ex)
        {
            _mainWindowViewModel.SetError("An error has occurred. Try again later.", 5000);
        }
    }

    public async void LoadProduct(int productId)
    {
        try
        {
            Console.WriteLine("Loading Product...");
            var response = await _ProductService.GetProductAsync(productId);

            if (response.IsSuccess)
            {
                Product = response.Data;
                Title = $"Edit product : {Product.name}";
                SelectedCate = $"Current category : {Product.category.name}";
            }
            else
            {
                _mainWindowViewModel.SetError($"An error has occurred: {response.ApiException?.Message ?? "Unknown error"}", 5000);
            }
        }
        catch (Exception ex)
        {
            _mainWindowViewModel.SetError("An error has occurred. Try again later.", 5000);
        }
    }



}