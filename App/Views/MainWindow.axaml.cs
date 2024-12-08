using System;
using App.Interfaces;
using App.Services;
using App.ViewModels;
using App.Views.Auth;
using App.Views.Pages;
using App.Views.Pages.Categories;
using App.Views.Pages.Clients;
using App.Views.Pages.Logs;
using App.Views.Pages.Orders;
using App.Views.Pages.Stocks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;

namespace App.Views;

public partial class MainWindow : Window
{
    private AuthService _authService;
    private DispatcherTimer _refreshTimer;
    private DispatcherTimer _countdownTimer;
    private int _remainingSeconds;
    private MainWindowViewModel _mainWindowViewModel;

    public MainWindow()
    {
        _authService = new AuthService();
        this._mainWindowViewModel = new MainWindowViewModel();
        this.DataContext = _mainWindowViewModel;
        InitializeComponent();

        InitializeNavigation();

        // Timer pour rafraîchir le contenu toutes les 30 secondes
        _refreshTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(30)
        };
        _refreshTimer.Tick += RefreshContent;
        _refreshTimer.Start();

        // Timer pour mettre à jour le texte du RefreshText chaque seconde
        _countdownTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        _countdownTimer.Tick += UpdateCountdownText;
        _countdownTimer.Start();

        _remainingSeconds = 30;

        NavigateToPage(new Home(this.DataContext as MainWindowViewModel));
    }

    private void InitializeNavigation()
    {
        var tab1Button = this.FindControl<Button>("Tab1Button"); // Home
        var tab6Button = this.FindControl<Button>("Tab6Button"); // Categories
        var tab2Button = this.FindControl<Button>("Tab2Button"); // Stocks
        var tab3Button = this.FindControl<Button>("Tab3Button"); // Orders
        var tab4Button = this.FindControl<Button>("Tab4Button"); // Clients
        var tab5Button = this.FindControl<Button>("Tab5Button"); // Logs

        tab1Button.Click += (_, _) => NavigateToPage(new Home(this.DataContext as MainWindowViewModel));
        tab6Button.Click += (_, _) => NavigateToPage(new CategoriesIndex(this.DataContext as MainWindowViewModel));
        tab2Button.Click += (_, _) => NavigateToPage(new StocksIndex(this.DataContext as MainWindowViewModel));
        tab3Button.Click += (_, _) => NavigateToPage(new OrdersIndex(this.DataContext as MainWindowViewModel));
        tab4Button.Click += (_, _) => NavigateToPage(new ClientsIndex(this.DataContext as MainWindowViewModel));
        tab5Button.Click += (_, _) => NavigateToPage(new LogsIndex(this.DataContext as MainWindowViewModel));
    }

    private void RefreshContent(object? sender, EventArgs e)
    {
        if (ContentArea.Content is IRefreshable refreshable)
        {
            refreshable.Refresh();
            _remainingSeconds = 30; // Réinitialiser le compteur après un rafraîchissement
        }
    }

    private void UpdateCountdownText(object? sender, EventArgs e)
    {
        var refreshText = this.FindControl<TextBlock>("RefreshText");

        if (ContentArea.Content is IRefreshable)
        {
            _mainWindowViewModel.CountDown = $"Content reloaded in {_remainingSeconds} second{( _remainingSeconds > 1 ? "s" : "")}"; 
            _remainingSeconds--;

            if (_remainingSeconds < 0)
            {
                _remainingSeconds = 30; // Boucle après 30 secondes
            }
        } else {
            _mainWindowViewModel.CountDown = string.Empty;
        }
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
        _remainingSeconds = 30;
    }
}
