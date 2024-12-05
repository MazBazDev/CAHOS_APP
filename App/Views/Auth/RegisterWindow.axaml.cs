using System;
using App.Models.Requests;
using App.Services;
using App.ViewModels;
using App.ViewModels.Auth;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace App.Views.Auth;

public partial class RegisterWindow : Window
{
    private readonly AuthService _authService;
    public RegisterWindow()
    {
        this.DataContext = new RegisterWindowViewModel();
        InitializeComponent();
        _authService = new AuthService();
    }

    private async void RegisterHandler(object? sender, RoutedEventArgs e)
    {
        var registerResult = await _authService.RegisterAsync(
            new RegisterRequest {
                name = NameInput.Text,
                email = EmailInput.Text,
                password = PasswordInput.Text
            }
        );

        if (registerResult.IsSuccess && registerResult.Data != null)
        {
            Console.WriteLine("Registration successful!");
            NavigateToHome();
        }
        else if (registerResult.ApiException != null)
        {
            ErrorMessage.Text = registerResult.ApiException.Message;
            return;
        }
        else if (registerResult.GeneralException != null)
        {
            Console.WriteLine($"Unexpected Error: {registerResult.GeneralException.Message}");
            ErrorMessage.Text = "An unexpected error occurred!";
            return;
        }
        
        ErrorMessage.Text = "An unexpected error occurred!";
    }

    private void goToLogin(object? sender, RoutedEventArgs e)
    {
        new LoginWindow().Show();
        this.Close();
    }
    
    private void NavigateToHome()
    {
        new MainWindow().Show();
        this.Close();
    }
}