using System;
using App.Models;
using App.Services;
using App.ViewModels;
using App.ViewModels.Pages.Orders;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;

namespace App.Views.Pages.Orders;

public partial class OrdersCreate : UserControl
{
    private readonly OrderService _OrderService;
    private readonly MainWindowViewModel _mainWindowViewModel;

    public OrdersCreate(MainWindowViewModel mainWindowViewModel)
    {
        _mainWindowViewModel = mainWindowViewModel;
        this.DataContext = new OrderCreateViewModel(mainWindowViewModel);
        _OrderService = new OrderService();
        InitializeComponent();
    }

    private async void StoreOrder(object? sender, RoutedEventArgs e)
    {
        try
        {
            var selectedClient = ClientInput.SelectedItem as Client;
            var selectedProduct = ProductInput.SelectedItem as Product;
            var selectedStatus = StatusInput.SelectedItem as string;

            if (selectedClient == null)
            {
                _mainWindowViewModel.SetError("Invalid client, please select one.", 5000);
                return;
            }
            
            if (selectedProduct == null)
            {
                _mainWindowViewModel.SetError("Invalid product, please select one.", 5000);
                return;
            }
            
            if (selectedStatus == null)
            {
                _mainWindowViewModel.SetError("Invalid status, please select one.", 5000);
                return;
            }
            
            var createOrderResult = await _OrderService.CreateOrderAsync(
                new Order {
                    client_id = selectedClient.id,
                    product_id = selectedProduct.id,
                    quantity = int.Parse(QuantityInput.Text),
                    status = selectedStatus,
                    order_date = DateTime.Now
                }
            );
            
            if (createOrderResult.IsSuccess && createOrderResult.Data != null)
            {
                _mainWindowViewModel.SetSuccess("Order created successfully!", 3000);
                var mainWindow = (MainWindow)this.FindAncestorOfType<Window>();
                mainWindow?.NavigateToPage(new OrdersIndex(_mainWindowViewModel));
            }
            else
            {
                var errorMessage = createOrderResult.ApiException?.Message 
                                   ?? createOrderResult.GeneralException?.Message 
                                   ?? "An unexpected error occurred!";
                _mainWindowViewModel.SetError(errorMessage, 5000);
            }
        }
        catch (Exception ex)
        {
            _mainWindowViewModel.SetError($"Error : {ex.Message}", 5000);
        }
    }

    private void updateTotal()
    {
        var selectedProduct = ProductInput.SelectedItem as Product;
        
        var quantity = int.Parse(QuantityInput.Text ?? "0");

        if (selectedProduct != null)
        {
            var total = selectedProduct?.price * quantity;
        
            TotalInput.Text = total.ToString();
        
            Console.WriteLine($"Total: {total}");    
        }
    }

    private void QtyChanged(object? sender, NumericUpDownValueChangedEventArgs e)
    {
        updateTotal();
    }

    private void ProductChanged(object? sender, SelectionChangedEventArgs e)
    {
        updateTotal();
    }
}
