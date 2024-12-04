using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.Models;

namespace App.Services;

public class ClientService
{
    private readonly ApiService _apiService;
    
    public ClientService()
    {
        _apiService = new ApiService();
    }
    
    public async Task<ApiResponse<List<Client>>> GetClientsAsync()
    {
        try
        {
            var response = await _apiService.GetAsync<List<Client>>("clients");
            return new ApiResponse<List<Client>>
            {
                IsSuccess = true,
                Data = response
            };
        } catch (ApiException e) {
            Console.WriteLine(e);
            return new ApiResponse<List<Client>>
            {
                IsSuccess = false,
                ApiException = e
            };
        } catch (Exception e) {
            Console.WriteLine(e);
            return new ApiResponse<List<Client>>
            {
                IsSuccess = false,
                GeneralException = e
            };
        }
    }
    
    public async Task<ApiResponse<Client>> GetClientAsync(int id)
    {
        try
        {
            var response = await _apiService.GetAsync<Client>($"clients/{id}");
            return new ApiResponse<Client>
            {
                IsSuccess = true,
                Data = response
            };
        } catch (ApiException e) {
            Console.WriteLine(e);
            return new ApiResponse<Client>
            {
                IsSuccess = false,
                ApiException = e
            };
        } catch (Exception e) {
            Console.WriteLine(e);
            return new ApiResponse<Client>
            {
                IsSuccess = false,
                GeneralException = e
            };
        }
    }
    
    public async Task<ApiResponse<Client>> CreateClientAsync(Client client)
    {
        try
        {
            var response = await _apiService.PostAsync<Client, Client>("clients", client);
            return new ApiResponse<Client>
            {
                IsSuccess = true,
                Data = response
            };
        } catch (ApiException e) {
            Console.WriteLine(e);
            return new ApiResponse<Client>
            {
                IsSuccess = false,
                ApiException = e
            };
        } catch (Exception e) {
            Console.WriteLine(e);
            return new ApiResponse<Client>
            {
                IsSuccess = false,
                GeneralException = e
            };
        }
    }
    
    public async Task<ApiResponse<Client>> UpdateClientAsync(int id, Client datas)
    {
        try
        {
            var response = await _apiService.PutAsync<Client, Client>($"clients/{id}", datas);
            return new ApiResponse<Client>
            {
                IsSuccess = true,
                Data = response
            };
        } catch (ApiException e) {
            Console.WriteLine(e);
            return new ApiResponse<Client>
            {
                IsSuccess = false,
                ApiException = e
            };
        } catch (Exception e) {
            Console.WriteLine(e);
            return new ApiResponse<Client>
            {
                IsSuccess = false,
                GeneralException = e
            };
        }
    }
    
    public async Task<ApiResponse<bool>> DeleteClientAsync(int id)
    {
        try
        {
            await _apiService.DeleteAsync($"clients/{id}");
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