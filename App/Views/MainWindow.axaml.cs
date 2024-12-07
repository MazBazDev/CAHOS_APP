using App.Services;
using App.ViewModels;
using App.ViewModels.Pages;
using App.Views.Auth;
using App.Views.Pages;
using App.Views.Pages.Clients;
using App.Views.Pages.Logs;
using App.Views.Pages.Orders;
using App.Views.Pages.Stocks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using ClientsIndex = App.Views.Pages.Clients.ClientsIndex;

namespace App.Views;

public partial class MainWindow : Window
{
    private AuthService _authService;
    public MainWindow()
    {
        this.DataContext = new MainWindowViewModel();
        InitializeComponent();
        
        _authService = new AuthService();
        
        var tab1Button = this.FindControl<Button>("Tab1Button"); // Home
        var tab2Button = this.FindControl<Button>("Tab2Button"); // Stocks
        var tab3Button = this.FindControl<Button>("Tab3Button"); // Orders
        var tab4Button = this.FindControl<Button>("Tab4Button"); // Clients
        var tab5Button = this.FindControl<Button>("Tab5Button"); // Logs
        
        var contentArea = this.FindControl<ContentControl>("ContentArea");

        tab1Button.Click += (_, _) => NavigateToPage(new home());
        tab2Button.Click += (_, _) => NavigateToPage(new StocksIndex(this.DataContext as MainWindowViewModel));
        tab3Button.Click += (_, _) => NavigateToPage(new OrdersIndex(this.DataContext as MainWindowViewModel));
        tab4Button.Click += (_, _) => NavigateToPage(new ClientsIndex(this.DataContext as MainWindowViewModel));
        tab5Button.Click += (_, _) => NavigateToPage(new LogsIndex());
    }

    private async void LogoutButton_OnClick(object? sender, RoutedEventArgs e)
    {
        await _authService.LogoutAsync();
        new LoginWindow().Show();
        this.Close();
    }
    
    public void NavigateToPage(UserControl page)
    {
        ContentArea.Content = page;
    }
}