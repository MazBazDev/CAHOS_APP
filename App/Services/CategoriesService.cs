using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Models;

namespace App.Services;

public class CategoriesService
{
    private readonly ApiService _apiService;
    
    public CategoriesService()
    {
        _apiService = new ApiService();
    }
    
    public async Task<ApiResponse<List<Category>>> GetCategoriesAsync()
    {
        try
        {
            var response = await _apiService.GetAsync<List<Category>>("categories");
            return new ApiResponse<List<Category>>
            {
                IsSuccess = true,
                Data = response
            };
        } catch (ApiException e) {
            Console.WriteLine(e);
            return new ApiResponse<List<Category>>
            {
                IsSuccess = false,
                ApiException = e
            };
        } catch (Exception e) {
            Console.WriteLine(e);
            return new ApiResponse<List<Category>>
            {
                IsSuccess = false,
                GeneralException = e
            };
        }
    }
    
    public async Task<ApiResponse<Category>> GetCategoryAsync(int id)
    {
        try
        {
            var response = await _apiService.GetAsync<Category>($"categories/{id}");
            return new ApiResponse<Category>
            {
                IsSuccess = true,
                Data = response
            };
        } catch (ApiException e) {
            Console.WriteLine(e);
            return new ApiResponse<Category>
            {
                IsSuccess = false,
                ApiException = e
            };
        } catch (Exception e) {
            Console.WriteLine(e);
            return new ApiResponse<Category>
            {
                IsSuccess = false,
                GeneralException = e
            };
        }
    }
    
    
    public async Task<ApiResponse<Category>> CreateCategoryAsync(Category category)
    {
        try
        {
            var response = await _apiService.PostAsync<Category, Category>("categories", category);
            return new ApiResponse<Category>
            {
                IsSuccess = true,
                Data = response
            };
        } catch (ApiException e) {
            Console.WriteLine(e);
            return new ApiResponse<Category>
            {
                IsSuccess = false,
                ApiException = e
            };
        } catch (Exception e) {
            Console.WriteLine(e);
            return new ApiResponse<Category>
            {
                IsSuccess = false,
                GeneralException = e
            };
        }
    }
    
    public async Task<ApiResponse<Category>> UpdateCategoryAsync(int id, Category data)
    {
        try
        {
            var response = await _apiService.PutAsync<Category, Category>($"categories/{id}", data);
            return new ApiResponse<Category>
            {
                IsSuccess = true,
                Data = response
            };
        } catch (ApiException e) {
            Console.WriteLine(e);
            return new ApiResponse<Category>
            {
                IsSuccess = false,
                ApiException = e
            };
        } catch (Exception e) {
            Console.WriteLine(e);
            return new ApiResponse<Category>
            {
                IsSuccess = false,
                GeneralException = e
            };
        }
    }
    
    public async Task<ApiResponse<bool>> DeleteCategoryAsync(int id)
    {
        try
        {
            await _apiService.DeleteAsync($"categories/{id}");
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