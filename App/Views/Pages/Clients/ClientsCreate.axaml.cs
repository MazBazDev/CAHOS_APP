using System;
using App.Models;
using App.Services;
using App.ViewModels;
using App.ViewModels.Pages.Clients;
using App.ViewModels.Pages.Stock;
using App.Views.Pages.Stocks;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;

namespace App.Views.Pages.Clients;

public partial class ClientsCreate : UserControl
{
    private readonly ClientService _clientService;
    private readonly MainWindowViewModel _mainWindowViewModel;

    public ClientsCreate(MainWindowViewModel mainWindowViewModel)
    {
        _mainWindowViewModel = mainWindowViewModel;
        this.DataContext = new ClientCreateViewModel();
        _clientService = new ClientService();
        InitializeComponent();
    }

    private async void StoreClient(object? sender, RoutedEventArgs e)
    {
        try
        {
            var createClientResult = await _clientService.CreateClientAsync(
                new Client
                {
                    name = NameInput.Text,
                    address = AddressInput.Text,
                    siret = SiretInput.Text
                }
            );
            
            if (createClientResult.IsSuccess && createClientResult.Data != null)
            {
                var mainWindow = (MainWindow)this.FindAncestorOfType<Window>();
                mainWindow?.NavigateToPage(new ClientsIndex(_mainWindowViewModel));
            }
            else
            {
                var errorMessage = createClientResult.ApiException?.Message 
                                   ?? createClientResult.GeneralException?.Message 
                                   ?? "An unexpected error occurred!";
                _mainWindowViewModel.SetError(errorMessage, 5000);
            }
        }
        catch (Exception ex)
        {
            _mainWindowViewModel.SetError($"Error : {ex.Message}", 5000);
        }
    }

    private void OnTextInput(object? sender, TextInputEventArgs e)
    {
        // Autorise uniquement les chiffres
        if (!int.TryParse(e.Text, out _))
        {
            e.Handled = true;
        }
    }
}
