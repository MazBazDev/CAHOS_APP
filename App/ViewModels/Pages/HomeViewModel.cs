using System;
using System.Collections.Generic;
using System.Linq;
using App.Models;
using App.Services;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
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
    
    
    private ISeries[] _series;
    
    public ISeries[] Series
    {
        get => _series;
        set => this.RaiseAndSetIfChanged(ref _series, value);
    }
    
    private Axis[] _xAxes;
    public Axis[] XAxes
    {
        get => _xAxes;
        set => this.RaiseAndSetIfChanged(ref _xAxes, value);
    }
   
    public HomeViewModel(MainWindowViewModel mainWindowViewModel)
    {
        _mainWindowViewModel = mainWindowViewModel;
        _homeService = new HomeService();

        GetDashboardAsync();
        GetAlertsAsync();
        GetStatsAsync();
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
        var response = await _homeService.GetStatsAsync();
        if (response.IsSuccess)
        {
            var stats = response.Data;
            
            Series = [
                new ColumnSeries<int>
                {
                    Values = stats.series,
                    XToolTipLabelFormatter = x =>  $"{x.Model.ToString()} orders on {stats.labels[x.Index].ToString("dd/MM/yyyy")}",
                }
            ];
        
            XAxes = new[]
            {
                new Axis
                {
                    Labels = stats.labels.Select(x => x.ToString("dd/MM/yyyy")).ToArray()
                }
            };
        } else
        {
            _mainWindowViewModel.SetError($"An error has occurred : {response.ApiException?.Message ?? "Erreur inconnue"}", 5000);
        }
        
    }
}