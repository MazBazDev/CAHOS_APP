using System.Collections.Generic;
using App.Models;
using App.Services;
using ReactiveUI;

namespace App.ViewModels.Pages;

public class HomeViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _mainWindowViewModel;
    private readonly HomeService _homeService;

    private Dashboard _dashboard;
    public Dashboard Dashboard
    {
        get => _dashboard;
        set => this.RaiseAndSetIfChanged(ref _dashboard, value);
    }
    
    private Alert _alerts;
    public Alert Alerts
    {
        get => _alerts;
        set => this.RaiseAndSetIfChanged(ref _alerts, value);

    }
   
    public HomeViewModel(MainWindowViewModel mainWindowViewModel)
    {
        _mainWindowViewModel = mainWindowViewModel;
        _homeService = new HomeService();

        GetDashboardAsync();
        GetAlertsAsync();
    }
    
    public async void GetDashboardAsync()
    {
        var response = await _homeService.GetDashboardAsync();
        if (response.IsSuccess)
        {
            Dashboard = response.Data;
        } else
        {
            _mainWindowViewModel.SetError($"An error has occurred : {response.ApiException?.Message ?? "Erreur inconnue"}", 5000);
        }
    }
    
    public async void GetAlertsAsync()
    {
        var response = await _homeService.GetAlertsAsync();
        if (response.IsSuccess)
        {
            Alerts = response.Data;
        } else
        {
            _mainWindowViewModel.SetError($"An error has occurred : {response.ApiException?.Message ?? "Erreur inconnue"}", 5000);
        }
    }

    public async void GetStatsAsync()
    {
        
    }
}