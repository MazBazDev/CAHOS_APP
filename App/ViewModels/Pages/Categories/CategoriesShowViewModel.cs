using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using App.Models;
using App.Services;
using ReactiveUI;

namespace App.ViewModels.Pages.Categories;

public class CategoriesShowViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _mainWindowViewModel;
    
    private readonly CategoriesService _categoryService;
    
    private Category _category = new();
    
    public Category Category
    {
        get => _category;
        set => this.RaiseAndSetIfChanged(ref _category, value);
    }
    
    private string _title = "Show order";
    public string Title
    {
        get  => _title;
        set => this.RaiseAndSetIfChanged(ref _title, value);
    }

    public CategoriesShowViewModel(MainWindowViewModel? mainWindowViewModel, Category category)
    {
        _mainWindowViewModel = mainWindowViewModel;
        _categoryService = new CategoriesService();
        
        LoadCategory(category);
    }
    
    public async void LoadCategory(Category category)
    {
        try
        {
            Console.WriteLine("Loading Category...");
            var response = await _categoryService.GetCategoryAsync(category.id);

            if (response.IsSuccess)
            {
                Category = response.Data;
                Title = $"Edit category : {Category.id}";
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