using System;
using System.Collections.ObjectModel;
using App.Models;
using App.Services;
using ReactiveUI;

namespace App.ViewModels.Pages.Categories;

public partial class CategoriesIndexViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _mainWindowViewModel;
    private readonly CategoriesService _categoriesService;
    
    public string Title => "Categories";
    
    private ObservableCollection<Category> _categories;

    public ObservableCollection<Category> Categories
    {
        get => _categories;
        set => this.RaiseAndSetIfChanged(ref _categories, value);
    }
    
    public CategoriesIndexViewModel(MainWindowViewModel mainWindowViewModel)
    {
        _mainWindowViewModel = mainWindowViewModel;
        _categoriesService = new CategoriesService();
        LoadCategoriesAsync();
    }

    public async void LoadCategoriesAsync()
    {
        try
        {
            Console.WriteLine("Loading orders...");
            var response = await _categoriesService.GetCategoriesAsync();

            if (response.IsSuccess)
            {
                Console.WriteLine("Categories loaded successfully!");
                Categories = new ObservableCollection<Category>(response.Data);
            } else {
                _mainWindowViewModel.SetError($"An error has occurred : {response.ApiException?.Message ?? "Erreur inconnue"}", 5000);
            }
        }
        catch (Exception ex) {
            _mainWindowViewModel.SetError($"An error has occurred. Try again later.", 5000);
        }
    }
}
