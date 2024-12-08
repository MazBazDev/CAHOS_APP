using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Models;

namespace App.Services;

public class LogService
{
    private readonly ApiService _apiService;
    
    public LogService()
    {
        _apiService = new ApiService();
    }
    
    public async Task<ApiResponse<List<Log>>> GetLogsAsync()
    {
        try
        {
            var response = await _apiService.GetAsync<List<Log>>("logs");
            return new ApiResponse<List<Log>>
            {
                IsSuccess = true,
                Data = response
            };
        } catch (ApiException e) {
            Console.WriteLine(e);
            return new ApiResponse<List<Log>>
            {
                IsSuccess = false,
                ApiException = e
            };
        } catch (Exception e) {
            Console.WriteLine(e);
            return new ApiResponse<List<Log>>
            {
                IsSuccess = false,
                GeneralException = e
            };
        }
    }
}