using System;
using System.Collections.ObjectModel;
using App.Models;
using App.Services;
using ReactiveUI;

namespace App.ViewModels.Pages.Clients;

public partial class ClientIndexViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _mainWindowViewModel;
    private readonly ClientService _clientService;
    
    public string Title => "Clients";
    
    private ObservableCollection<Client> _clients;

    public ObservableCollection<Client> Clients
    {
        get => _clients;
        set => this.RaiseAndSetIfChanged(ref _clients, value);
    }
    
    public ClientIndexViewModel(MainWindowViewModel mainWindowViewModel)
    {
        _mainWindowViewModel = mainWindowViewModel;
        _clientService = new ClientService();
        LoadClientsAsync();
    }

    public async void LoadClientsAsync()
    {
        try
        {
            Console.WriteLine("Loading clients...");
            var response = await _clientService.GetClientsAsync();

            if (response.IsSuccess)
            {
                Console.WriteLine("Clients loaded successfully!");
                Clients = new ObservableCollection<Client>(response.Data);
            } else {
                _mainWindowViewModel.SetError($"An error has occurred : {response.ApiException?.Message ?? "Erreur inconnue"}", 5000);
            }
        }
        catch (Exception ex) {
            _mainWindowViewModel.SetError($"An error has occurred. Try again later.", 5000);
        }
    }
}
