using System;
using App.Models;
using App.Services;
using App.ViewModels;
using App.ViewModels.Pages.Orders;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;

namespace App.Views.Pages.Orders;

public partial class OrderShow : UserControl
{
    private readonly MainWindowViewModel _mainWindowViewModel;
    private readonly OrderService _OrderService;
    private readonly Order _originalOrder;

    public OrderShow(MainWindowViewModel mainWindowViewModel, Order order)
    {
        _mainWindowViewModel = mainWindowViewModel;
        this.DataContext = new OrderShowViewModel(mainWindowViewModel, order);
        _OrderService = new OrderService();
        _originalOrder = order;
        
        InitializeComponent();
    }

    private async void DeleteOrder(object? sender, RoutedEventArgs e)
    {
        var deleteProductResult = await _OrderService.DeleteOrderAsync(_originalOrder.id);
        
        if (deleteProductResult.IsSuccess)
        {
            _mainWindowViewModel.SetSuccess("Order deleted successfully!", 3000);
            
            var mainWindow = (MainWindow)this.FindAncestorOfType<Window>();
            mainWindow?.NavigateToPage(new Orders.OrdersIndex(_mainWindowViewModel));
        }
        else
        {
            var errorMessage = deleteProductResult.ApiException?.Message 
                               ?? deleteProductResult.GeneralException?.Message 
                               ?? "An unexpected error occurred!";
            _mainWindowViewModel.SetError(errorMessage, 5000);
        }
    }

    private async void UpdateOrder(object? sender, RoutedEventArgs e)
{
    try
    {
        var updateOrder = new Order();
        
        var selectedClient = ClientInput.SelectedItem as Client;
        if (selectedClient != null && selectedClient.id != _originalOrder.client_id)
        {
            updateOrder.client_id = selectedClient.id;
        }
        
        var selectedProduct = ProductInput.SelectedItem as Product;
        if (selectedProduct != null && selectedProduct.id != _originalOrder.product_id)
        {
            updateOrder.product_id = selectedProduct.id;
        }
        
        var selectedStatus = StatusInput.SelectedItem as string;
        if (selectedStatus != null && selectedStatus != _originalOrder.status)
        {
            updateOrder.status = selectedStatus;
        }
        
        var quantity = int.Parse(QuantityInput.Text ?? "0");
        
        if (quantity != _originalOrder.quantity)
        {
            updateOrder.quantity = quantity;
        }
        
        var updateOrderResult = await _OrderService.UpdateOrderAsync(_originalOrder.id, updateOrder);
        
        if (updateOrderResult.IsSuccess && updateOrderResult.Data != null)
        {
            _mainWindowViewModel.SetSuccess("Order updated successfully!", 3000);
            
            var mainWindow = (MainWindow)this.FindAncestorOfType<Window>();
            mainWindow?.NavigateToPage(new Orders.OrdersIndex(_mainWindowViewModel));
        }
        else
        {
            var errorMessage = updateOrderResult.ApiException?.Message 
                               ?? updateOrderResult.GeneralException?.Message 
                               ?? "An unexpected error occurred!";
            _mainWindowViewModel.SetError(errorMessage, 5000);
        }
    }
    catch (Exception ex)
    {
        _mainWindowViewModel.SetError($"Error: {ex.Message}", 5000);
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