using App.Interfaces;
using App.Models;
using App.Services;
using App.ViewModels;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using ClientIndexViewModel = App.ViewModels.Pages.Clients.ClientIndexViewModel;

namespace App.Views.Pages.Clients;

public partial class ClientsIndex : UserControl, IRefreshable
{
    private readonly MainWindowViewModel _mainWindowViewModel;
    
    public ClientsIndex(MainWindowViewModel mainWindowViewModel)
    {
        _mainWindowViewModel = mainWindowViewModel;
        this.DataContext = new ClientIndexViewModel(mainWindowViewModel);
        InitializeComponent();
    }

    private void AddClientButton(object? sender, RoutedEventArgs routedEventArgs)
    {
        var mainWindow = (MainWindow)this.FindAncestorOfType<Window>();
        mainWindow?.NavigateToPage(new Clients.ClientsCreate(_mainWindowViewModel));
    }

    private void ShowClient(object? sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count == 0) return;
        var client = (Client)e.AddedItems[0];
        var mainWindow = (MainWindow)this.FindAncestorOfType<Window>();
        mainWindow?.NavigateToPage(new ClientsShow(_mainWindowViewModel, client));
    }

    public void Refresh()
    {
        this.DataContext = new ClientIndexViewModel(_mainWindowViewModel);
    }
}