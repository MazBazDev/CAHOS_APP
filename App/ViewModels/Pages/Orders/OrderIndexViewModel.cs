using System;
using System.Collections.ObjectModel;
using App.Models;
using App.Services;
using ReactiveUI;

namespace App.ViewModels.Pages.Orders;

public partial class OrderIndexViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _mainWindowViewModel;
    private readonly OrderService _orderService;
    
    public string Title => "Orders";
    
    private ObservableCollection<Order> _orders;

    public ObservableCollection<Order> Orders
    {
        get => _orders;
        set => this.RaiseAndSetIfChanged(ref _orders, value);
    }
    
    public OrderIndexViewModel(MainWindowViewModel mainWindowViewModel)
    {
        _mainWindowViewModel = mainWindowViewModel;
        _orderService = new OrderService();
        LoadOrdersAsync();
    }

    public async void LoadOrdersAsync()
    {
        try
        {
            Console.WriteLine("Loading orders...");
            var response = await _orderService.GetOrdersAsync();

            if (response.IsSuccess)
            {
                Console.WriteLine("Orders loaded successfully!");
                Orders = new ObservableCollection<Order>(response.Data);
            } else {
                _mainWindowViewModel.SetError($"An error has occurred : {response.ApiException?.Message ?? "Erreur inconnue"}", 5000);
            }
        }
        catch (Exception ex) {
            _mainWindowViewModel.SetError($"An error has occurred. Try again later.", 5000);
        }
    }
}
