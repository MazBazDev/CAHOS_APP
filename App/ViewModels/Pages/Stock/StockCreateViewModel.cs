using System;
using System.Collections.ObjectModel;
using App.Models;
using App.Services;
using ReactiveUI;

namespace App.ViewModels.Pages.Stock;

public class StockCreateViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _mainWindowViewModel;
    private readonly CategoriesService _categoriesService;
    
    public string Title => "Create a new product";
    
    private ObservableCollection<Category> _categories = new();

    public ObservableCollection<Category> Categories
    {
        get => _categories;
        set => this.RaiseAndSetIfChanged(ref _categories, value);
    }
    
    
    public StockCreateViewModel(MainWindowViewModel? mainWindowViewModel)
    {
        _mainWindowViewModel = mainWindowViewModel;
        _categoriesService = new CategoriesService();
        Categories = new ObservableCollection<Category>();
        LoadCategoriesAsync();
    }
    
    public async void LoadCategoriesAsync()
    {
        try
        {
            Console.WriteLine("Loading products...");
            var response = await _categoriesService.GetCategoriesAsync();

            if (response.IsSuccess)
            {
                Console.WriteLine("Categoties loaded successfully!");
                
                Categories.Clear();
                foreach (var category in response.Data)
                {
                    Categories.Add(category);
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