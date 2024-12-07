using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Models;

namespace App.Services;

public class HomeService
{
    private readonly ApiService _apiService;
    
    public HomeService()
    {
        _apiService = new ApiService();
    }
    
    public async Task<ApiResponse<Dashboard>> GetDashboardAsync()
    {
        try
        {
            var response = await _apiService.GetAsync<Dashboard>("dashboard");
            return new ApiResponse<Dashboard>
            {
                IsSuccess = true,
                Data = response
            };
        } catch (ApiException e) {
            Console.WriteLine(e);
            return new ApiResponse<Dashboard>
            {
                IsSuccess = false,
                ApiException = e
            };
        } catch (Exception e) {
            Console.WriteLine(e);
            return new ApiResponse<Dashboard>
            {
                IsSuccess = false,
                GeneralException = e
            };
        }
    }
    
    public async Task<ApiResponse<Alert>> GetAlertsAsync()
    {
        try
        {
            var response = await _apiService.GetAsync<Alert>("alerts");
            return new ApiResponse<Alert>
            {
                IsSuccess = true,
                Data = response
            };
        } catch (ApiException e) {
            Console.WriteLine(e);
            return new ApiResponse<Alert>
            {
                IsSuccess = false,
                ApiException = e
            };
        } catch (Exception e) {
            Console.WriteLine(e);
            return new ApiResponse<Alert>
            {
                IsSuccess = false,
                GeneralException = e
            };
        }
    }
    
    public async Task<ApiResponse<List<Statistic>>> GetStatsAsync()
    {
        try
        {
            var response = await _apiService.GetAsync<List<Statistic>>("stats");
            return new ApiResponse<List<Statistic>>
            {
                IsSuccess = true,
                Data = response
            };
        } catch (ApiException e) {
            Console.WriteLine(e);
            return new ApiResponse<List<Statistic>>
            {
                IsSuccess = false,
                ApiException = e
            };
        } catch (Exception e) {
            Console.WriteLine(e);
            return new ApiResponse<List<Statistic>>
            {
                IsSuccess = false,
                GeneralException = e
            };
        }
    }
}