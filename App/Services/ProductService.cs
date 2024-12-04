using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Models;

namespace App.Services;

public class ProductService
{
    private readonly ApiService _apiService;
    
    public ProductService()
    {
        _apiService = new ApiService();
    }
    
    public async Task<ApiResponse<List<Product>>> GetProductsAsync()
    {
        try
        {
            var response = await _apiService.GetAsync<List<Product>>("products");
            return new ApiResponse<List<Product>>
            {
                IsSuccess = true,
                Data = response
            };
        } catch (ApiException e) {
            Console.WriteLine(e);
            return new ApiResponse<List<Product>>
            {
                IsSuccess = false,
                ApiException = e
            };
        } catch (Exception e) {
            Console.WriteLine(e);
            return new ApiResponse<List<Product>>
            {
                IsSuccess = false,
                GeneralException = e
            };
        }
    }
    
    public async Task<ApiResponse<Product>> GetProductAsync(int id)
    {
        try
        {
            var response = await _apiService.GetAsync<Product>($"products/{id}");
            return new ApiResponse<Product>
            {
                IsSuccess = true,
                Data = response
            };
        } catch (ApiException e) {
            Console.WriteLine(e);
            return new ApiResponse<Product>
            {
                IsSuccess = false,
                ApiException = e
            };
        } catch (Exception e) {
            Console.WriteLine(e);
            return new ApiResponse<Product>
            {
                IsSuccess = false,
                GeneralException = e
            };
        }
    }
    
    public async Task<ApiResponse<Product>> CreateProductAsync(Product product)
    {
        try
        {
            var response =  await _apiService.PostAsync<Product, Product>("products", product);
            return new ApiResponse<Product>
            {
                IsSuccess = true,
                Data = response
            };
        } catch (ApiException e) {
            Console.WriteLine(e);
            return new ApiResponse<Product>
            {
                IsSuccess = false,
                ApiException = e
            };
        } catch (Exception e) {
            Console.WriteLine(e);
            return new ApiResponse<Product>
            {
                IsSuccess = false,
                GeneralException = e
            };
        }
    }
    
    public async Task<ApiResponse<Product>> UpdateProductAsync(int id, Product data)
    {
        try
        {
            var response = await _apiService.PutAsync<Product, Product> ($"products/{id}", data);
            return new ApiResponse<Product>
            {
                IsSuccess = true,
                Data = response
            };
        } catch (ApiException e) {
            Console.WriteLine(e);
            return new ApiResponse<Product>
            {
                IsSuccess = false,
                ApiException = e
            };
        } catch (Exception e) {
            Console.WriteLine(e);
            return new ApiResponse<Product>
            {
                IsSuccess = false,
                GeneralException = e
            };
        }
    }
    
    public async Task<ApiResponse<bool>> DeleteProductAsync(int id)
    {
        try
        {
            await _apiService.DeleteAsync($"products/{id}");
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
    
    public async Task<ApiResponse<List<Product>>> GetProductsByCategoryAsync(int categoryId)
    {
        try
        {
            var response = await _apiService.GetAsync<List<Product>>($"products/category/{categoryId}");
            return new ApiResponse<List<Product>>
            {
                IsSuccess = true,
                Data = response
            };
        } catch (ApiException e) {
            Console.WriteLine(e);
            return new ApiResponse<List<Product>>
            {
                IsSuccess = false,
                ApiException = e
            };
        } catch (Exception e) {
            Console.WriteLine(e);
            return new ApiResponse<List<Product>>
            {
                IsSuccess = false,
                GeneralException = e
            };
        }
    }
}