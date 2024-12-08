using System;
using App.Models;
using App.Services;
using App.ViewModels;
using App.ViewModels.Pages.Clients;
using App.Views.Pages.Orders;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;

namespace App.Views.Pages.Clients;

public partial class ClientsShow : UserControl
{
    private readonly MainWindowViewModel _mainWindowViewModel;
    private readonly ClientService _ClientService;
    private readonly Client _originalClient;

    public ClientsShow(MainWindowViewModel mainWindowViewModel, Client client)
    {
        _mainWindowViewModel = mainWindowViewModel;
        this.DataContext = new ClientShowViewModel(mainWindowViewModel, client.id);
        _ClientService = new ClientService();
        _originalClient = client;
        
        InitializeComponent();
    }

    private void OnTextInput(object? sender, TextInputEventArgs e)
    {
        // Autorise uniquement les chiffres
        if (!int.TryParse(e.Text, out _))
        {
            e.Handled = true;
        }
    }

    private async void DeleteClient(object? sender, RoutedEventArgs e)
    {
        var deleteClientResult = await _ClientService.DeleteClientAsync(_originalClient.id);
        
        if (deleteClientResult.IsSuccess)
        {
            _mainWindowViewModel.SetSuccess("Client deleted successfully!", 3000);
        
            // Naviguer vers l'index des produits
            var mainWindow = (MainWindow)this.FindAncestorOfType<Window>();
            mainWindow?.NavigateToPage(new Clients.ClientsIndex(_mainWindowViewModel));
        }
        else
        {
            var errorMessage = deleteClientResult.ApiException?.Message 
                               ?? deleteClientResult.GeneralException?.Message 
                               ?? "An unexpected error occurred!";
            _mainWindowViewModel.SetError(errorMessage, 5000);
        }
    }

    private async void UpdateClient(object? sender, RoutedEventArgs e) {
        
        try {
            var updateClient = new Client();
            
           
            if (!string.IsNullOrWhiteSpace(NameInput.Text) && NameInput.Text != _originalClient.name)
            {
                updateClient.name = NameInput.Text;
            }
           
            if (!string.IsNullOrWhiteSpace(AddressInput.Text) && AddressInput.Text != _originalClient.address)
            {
                updateClient.address = AddressInput.Text;
            }
           
            if (!string.IsNullOrWhiteSpace(SiretInput.Text) && SiretInput.Text != _originalClient.siret)
            {
                updateClient.siret = SiretInput.Text;
            }
            
            var updateClientResult = await _ClientService.UpdateClientAsync(_originalClient.id, updateClient);
            
            if (updateClientResult.IsSuccess && updateClientResult.Data != null)
            {
                _mainWindowViewModel.SetSuccess("Client updated successfully!", 3000);
            
                // Naviguer vers l'index des produits
                var mainWindow = (MainWindow)this.FindAncestorOfType<Window>();
                mainWindow?.NavigateToPage(new Clients.ClientsIndex(_mainWindowViewModel));
            }
            else
            {
                var errorMessage = updateClientResult.ApiException?.Message 
                                   ?? updateClientResult.GeneralException?.Message 
                                   ?? "An unexpected error occurred!";
                _mainWindowViewModel.SetError(errorMessage, 5000);
            }
        } catch (Exception ex) {
            _mainWindowViewModel.SetError($"Error: {ex.Message}", 5000);
        }
    }

    private void ShowOrder(object? sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count == 0) return;
        var order = (Order)e.AddedItems[0];
        var mainWindow = (MainWindow)this.FindAncestorOfType<Window>();
        mainWindow?.NavigateToPage(new OrderShow(_mainWindowViewModel, order));
    }
}