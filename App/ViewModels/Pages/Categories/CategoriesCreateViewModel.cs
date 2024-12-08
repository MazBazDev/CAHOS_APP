using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using App.Models;
using App.Services;
using ReactiveUI;

namespace App.ViewModels.Pages.Categories;

public class CategoriesCreateViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _mainWindowViewModel;
    public string Title => "Create a new category";
    
    public CategoriesCreateViewModel(MainWindowViewModel? mainWindowViewModel)
    {
        _mainWindowViewModel = mainWindowViewModel;
    }
}