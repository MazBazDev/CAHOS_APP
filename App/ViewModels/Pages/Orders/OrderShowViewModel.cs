using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using App.Models;
using App.Services;
using ReactiveUI;

namespace App.ViewModels.Pages.Orders;

public class OrderShowViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _mainWindowViewModel;
    
    private readonly ClientService _clientService;
    private readonly ProductService _productService;
    private readonly OrderService _orderService;
    
    public List<string> Statuses => new List<string> { "Pending", "Delivered", "Cancelled" };
    
    private string _total = "0";
    
    public string Total
    {
        get => _total;
        set => this.RaiseAndSetIfChanged(ref _total, value);
    }
    
    private string _quantity = "0";
    
    public string Quantity
    {
        get => _quantity;
        set => this.RaiseAndSetIfChanged(ref _quantity, value);
    }
    
    private Order _order = new();
    
    public Order Order
    {
        get => _order;
        set => this.RaiseAndSetIfChanged(ref _order, value);
    }
    
    private ObservableCollection<Client> _clients = new();

    public ObservableCollection<Client> Clients
    {
        get => _clients;
        set => this.RaiseAndSetIfChanged(ref _clients, value);
    }
    
    private ObservableCollection<Product> _products = new();

    public ObservableCollection<Product> Products
    {
        get => _products;
        set => this.RaiseAndSetIfChanged(ref _products, value);
    }
    
    private string _title = "Show order";
    
    public string Title
    {
        get  => _title;
        set => this.RaiseAndSetIfChanged(ref _title, value);
    }

    public OrderShowViewModel(MainWindowViewModel? mainWindowViewModel, Order order)
    {
        _mainWindowViewModel = mainWindowViewModel;
        _clientService = new ClientService();
        _productService = new ProductService();
        _orderService = new OrderService();
        
        LoadProductsAsync();
        LoadClientsAsync();
        LoadOrder(order);
    }
    
    private async void LoadProductsAsync()
    {
        try
        {
            Console.WriteLine("Loading products...");
            var response = await _productService.GetProductsAsync();

            if (response.IsSuccess)
            {
                Console.WriteLine("Products loaded successfully!");
                
                Products.Clear();
                foreach (var product in response.Data)
                {
                    Products.Add(product);
                }
            } else {
                _mainWindowViewModel.SetError($"An error has occurred : {response.ApiException?.Message ?? "Erreur inconnue"}", 5000);
            }
        }
        catch (Exception ex) {
            _mainWindowViewModel.SetError($"An error has occurred. Try again later.", 5000);
        }
    }

    private async void LoadClientsAsync()
    {
        try
        {
            Console.WriteLine("Loading clients...");
            var response = await _clientService.GetClientsAsync();

            if (response.IsSuccess)
            {
                Console.WriteLine("Clients loaded successfully!");
                
                Clients.Clear();
                foreach (var clients in response.Data)
                {
                    Clients.Add(clients);
                }
            } else {
                _mainWindowViewModel.SetError($"An error has occurred : {response.ApiException?.Message ?? "Erreur inconnue"}", 5000);
            }
        }
        catch (Exception ex) {
            _mainWindowViewModel.SetError($"An error has occurred. Try again later.", 5000);
        }
    }

    public async void LoadOrder(Order order)
    {
        try
        {
            Console.WriteLine("Loading Product...");
            var response = await _orderService.GetOrderAsync(order.id);

            if (response.IsSuccess)
            {
                Order = response.Data;
                Title = $"Edit order : {Order.id}";
                Quantity = Order.quantity.ToString();
                Total = (Order.product?.price * Order.quantity).ToString();
            }
            else
            {
                _mainWindowViewModel.SetError($"An error has occurred: {response.ApiException?.Message ?? "Unknown error"}", 5000);
            }
        }
        catch (Exception ex)
        {
            _mainWindowViewModel.SetError("An error has occurred. Try again later.", 5000);
        }
    }



}