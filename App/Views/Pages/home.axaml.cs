using App.ViewModels;
using App.ViewModels.Pages;
using Avalonia.Controls;

namespace App.Views.Pages;

public partial class Home : UserControl
{
    private readonly MainWindowViewModel _mainWindowViewModel;
    
    public Home(MainWindowViewModel mainWindowViewModel)
    {
        _mainWindowViewModel = mainWindowViewModel;
        this.DataContext = new HomeViewModel(mainWindowViewModel);
        InitializeComponent();
    }
}