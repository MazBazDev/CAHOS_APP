using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Models;

namespace App.Services;

public class OrderService
{
    private readonly ApiService _apiService;
    
    public OrderService()
    {
        _apiService = new ApiService();
    }
    
    
    public async Task<ApiResponse<List<Order>>> GetOrdersAsync()
    {
        try
        {
            var response = await _apiService.GetAsync<List<Order>>("orders");
            return new ApiResponse<List<Order>>
            {
                IsSuccess = true,
                Data = response
            };
        } catch (ApiException e) {
            Console.WriteLine(e);
            return new ApiResponse<List<Order>>
            {
                IsSuccess = false,
                ApiException = e
            };
        } catch (Exception e) {
            Console.WriteLine(e);
            return new ApiResponse<List<Order>>
            {
                IsSuccess = false,
                GeneralException = e
            };
        }
    }
    
    public async Task<ApiResponse<Order>> GetOrderAsync(int id)
    {
        try
        {
            var response = await _apiService.GetAsync<Order>($"orders/{id}");
            return new ApiResponse<Order>
            {
                IsSuccess = true,
                Data = response
            };
        } catch (ApiException e) {
            Console.WriteLine(e);
            return new ApiResponse<Order>
            {
                IsSuccess = false,
                ApiException = e
            };
        } catch (Exception e) {
            Console.WriteLine(e);
            return new ApiResponse<Order>
            {
                IsSuccess = false,
                GeneralException = e
            };
        }
    }
    
    public async Task<ApiResponse<Order>> CreateOrderAsync(Order order)
    {
        try
        {
            var response = await _apiService.PostAsync<Order, Order>("orders", order);
            return new ApiResponse<Order>
            {
                IsSuccess = true,
                Data = response
            };
        } catch (ApiException e) {
            Console.WriteLine(e);
            return new ApiResponse<Order>
            {
                IsSuccess = false,
                ApiException = e
            };
        } catch (Exception e) {
            Console.WriteLine(e);
            return new ApiResponse<Order>
            {
                IsSuccess = false,
                GeneralException = e
            };
        }
    }
    
    public async Task<ApiResponse<Order>> UpdateOrderAsync(int id, Order data)
    {
        try
        {
            var response = await _apiService.PutAsync<Order, Order>($"orders/{id}", data);
            return new ApiResponse<Order>
            {
                IsSuccess = true,
                Data = response
            };
        } catch (ApiException e) {
            Console.WriteLine(e);
            return new ApiResponse<Order>
            {
                IsSuccess = false,
                ApiException = e
            };
        } catch (Exception e) {
            Console.WriteLine(e);
            return new ApiResponse<Order>
            {
                IsSuccess = false,
                GeneralException = e
            };
        }
    }
    
    public async Task<ApiResponse<bool>> DeleteOrderAsync(int id)
    {
        try
        {
            await _apiService.DeleteAsync($"orders/{id}");
            return new ApiResponse<bool>
            {
                IsSuccess = true,
                Data = true
            };
        } catch (ApiException e) {
            Console.WriteLine(e);
            return new ApiResponse<bool>
            {
                IsSuccess = false,
                ApiException = e
            };
        } catch (Exception e) {
            Console.WriteLine(e);
            return new ApiResponse<bool>
            {
                IsSuccess = false,
                GeneralException = e
            };
        }
    }
}