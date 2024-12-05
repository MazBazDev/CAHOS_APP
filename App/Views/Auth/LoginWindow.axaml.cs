using System;
using System.Threading.Tasks;
using App.Models.Requests;
using App.Services;
using App.ViewModels;
using App.ViewModels.Auth;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace App.Views.Auth;

public partial class LoginWindow : Window
{
    private readonly AuthService _authService;
    public LoginWindow()
    {
        this.DataContext = new LoginWindowViewModel();
        InitializeComponent();
        
        _authService = new AuthService();
    }

    private async void LoginHandler(object? sender, RoutedEventArgs e)
    {
        var loginResult = await _authService.LoginAsync(
            new LoginRequest { email = EmailInput.Text, password = PasswordInput.Text }
            );

        if (loginResult.IsSuccess && loginResult.Data != null)
        {
            Console.WriteLine("Login successful!");
            NavigateToHome();
        }
        else if (loginResult.ApiException != null)
        {
            ErrorMessage.Text = loginResult.ApiException.Message;
            return;
        }
        else if (loginResult.GeneralException != null)
        {
            Console.WriteLine($"Unexpected Error: {loginResult.GeneralException.Message}");
            ErrorMessage.Text = "An unexpected error occurred!";
            return;
        }
        
        ErrorMessage.Text = "An unexpected error occurred!";
    }
    
    private void goToRegister(object? sender, RoutedEventArgs e)
    {
        new RegisterWindow().Show();
        this.Close();
    }
    
    private void NavigateToHome()
    {
        new MainWindow().Show();
        this.Close();
    }
}