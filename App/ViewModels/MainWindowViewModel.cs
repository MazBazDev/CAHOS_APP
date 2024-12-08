using System;
using System.Threading.Tasks;
using App.Models;
using App.Services;
using ReactiveUI;

namespace App.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{ 
    private readonly AuthService _authService;
    
    private string? _errorMessage;
    private string? _errorColor;
    private string? _countDown;
    
    private User _user = new();

    public User User
    {
        get => _user;
        set => this.RaiseAndSetIfChanged(ref _user, value);
    }
    
    public string? ErrorMessage
    {
        get => _errorMessage;
        set => this.RaiseAndSetIfChanged(ref _errorMessage, value);
    }
    
    public string? ErrorColor
    {
        get => _errorColor;
        set => this.RaiseAndSetIfChanged(ref _errorColor, value);
    }
    
    public string? CountDown
    {
        get => _countDown;
        set => this.RaiseAndSetIfChanged(ref _countDown, value);
    }
    
    public MainWindowViewModel()
    {
        _authService = new AuthService();
        loadUserData();
    }
    
    public void SetError(string message, int? delay = null)
    {
        ErrorMessage = message;
        ErrorColor = "red";
        
        if (delay != null)
        {
            Task.Delay((int)delay).ContinueWith(_ => ClearError());
        }
    }
    
    public void SetSuccess(string message, int? delay = null)
    {
        ErrorMessage = message;
        ErrorColor = "green";
        
        if (delay != null)
        {
            Task.Delay((int)delay).ContinueWith(_ => ClearError());
        }
    }
    
    public void ClearError()
    {
        ErrorMessage = null;
        ErrorColor = null;
    }
    
    public async void  loadUserData()
    {
        try
        {
            var response = await _authService.GetUserAsync();

            if (response.IsSuccess)
            {
                User = response.Data;
            } else {
                SetError($"An error has occurred : {response.ApiException?.Message ?? "Erreur inconnue"}", 5000);
            }
        }
        catch (Exception ex) {
            SetError($"An error has occurred. Try again later.", 5000);
        }
    } 
}