using System;
using System.Collections.ObjectModel;
using App.Models;
using App.Services;
using ReactiveUI;

namespace App.ViewModels.Pages;

public class LogViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _mainWindowViewModel;
    private readonly LogService  _logService;
    
    private ObservableCollection<Log> _logs;
    
    public ObservableCollection<Log> Logs
    {
        get => _logs;
        set => this.RaiseAndSetIfChanged(ref _logs, value);
    }
    
    public string Title => "Logs";
    
    public LogViewModel(MainWindowViewModel mainWindowViewModel)
    {
        _mainWindowViewModel = mainWindowViewModel;
        _logService = new LogService();
        LoadLogsAsync();
    }
    
    
    public async void LoadLogsAsync()
    {
        try
        {
            Console.WriteLine("Loading logs...");
            var response = await _logService.GetLogsAsync();
            
            if (response.IsSuccess)
            {
                Console.WriteLine("Logs loaded successfully!");
                Logs = new ObservableCollection<Log>(response.Data);
            } else {
                _mainWindowViewModel.SetError($"An error has occurred : {response.ApiException?.Message ?? "Erreur inconnue"}", 5000);
            }
        }
        catch (Exception ex) {
            _mainWindowViewModel.SetError($"An error has occurred. Try again later.", 5000);
        }
    }
    
}