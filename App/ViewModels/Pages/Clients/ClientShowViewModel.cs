using System;
using System.Collections.ObjectModel;
using App.Models;
using App.Services;
using App.Views;
using App.Views.Pages.Clients;
using Avalonia.Controls;
using ReactiveUI;

namespace App.ViewModels.Pages.Clients;

public class ClientShowViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _mainWindowViewModel;
    private readonly ClientService _ClientService;
    
    private Client _product = new();
    private string _title = "Edit Client";
    
    public string Title
    {
        get  => _title;
        set => this.RaiseAndSetIfChanged(ref _title, value);
    }

    public Client Client
    {
        get => _product;
        set => this.RaiseAndSetIfChanged(ref _product, value);
    }


    public ClientShowViewModel(MainWindowViewModel? mainWindowViewModel, int clientId)
    {
        _mainWindowViewModel = mainWindowViewModel;
        _ClientService = new ClientService();
        Client = new Client();
        
        LoadClient(clientId);
    }

    public async void LoadClient(int productId)
    {
        try
        {
            Console.WriteLine("Loading Client...");
            var response = await _ClientService.GetClientAsync(productId);

            if (response.IsSuccess)
            {
                Client = response.Data;
                Title = $"Edit client : {Client.name}";
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