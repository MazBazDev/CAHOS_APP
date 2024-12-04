using System;
using System.Threading.Tasks;
using App.Models;
using App.Models.Requests;
using Exception = System.Exception;

namespace App.Services;

public class AuthService
{
    private readonly ApiService _apiService;

    public static Session? Session { get; private set; }

    public AuthService()
    {
        _apiService = new ApiService();
    }

    public async Task<ApiResponse<LoginResponse>> LoginAsync(LoginRequest request)
    {
        try
        {
            var response = await _apiService.PostAsync<LoginRequest, LoginResponse>("auth/login", request);
            Session = new Session(accessToken: response.access_token, tokenType: response.token_type);
            
            return new ApiResponse<LoginResponse>
            {
                IsSuccess = true,
                Data = response
            };
        }
        catch (ApiException ex)
        {
            Console.WriteLine($"[AuthService] API Error: {ex.Message}");
            return new ApiResponse<LoginResponse>
            {
                IsSuccess = false,
                ApiException = ex
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[AuthService] Unexpected Error: {ex.Message}");
            return new ApiResponse<LoginResponse>
            {
                IsSuccess = false,
                GeneralException = ex
            };
        }
    }

    public async Task<ApiResponse<RegisterResponse>> RegisterAsync(RegisterRequest request)
    {
        try
        {
            var response = await _apiService.PostAsync<RegisterRequest, RegisterResponse>("auth/register", request);
            Session = new Session(accessToken: response.access_token, tokenType: response.token_type);

            return new ApiResponse<RegisterResponse>
            {
                IsSuccess = true,
                Data = response,
            };
        }
        catch (ApiException ex)
        {
            Console.WriteLine($"[AuthService] API Error: {ex.Message}");
            return new ApiResponse<RegisterResponse>
            {
                IsSuccess = false,
                ApiException = ex
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[AuthService] Unexpected Error: {ex.Message}");
            return new ApiResponse<RegisterResponse>
            {
                IsSuccess = false,
                GeneralException = ex
            };
        }
    }

    public async Task<ApiResponse<bool>> LogoutAsync()
    {
        try
        {
            await _apiService.PostAsync<object, object>("auth/logout", null);
            Session = null;

            return new ApiResponse<bool>
            {
                IsSuccess = true,
                Data = true
            };
        }
        catch (ApiException ex)
        {
            Console.WriteLine($"[AuthService] API Error: {ex.Message}");
            return new ApiResponse<bool>
            {
                IsSuccess = false,
                ApiException = ex
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[AuthService] Unexpected Error: {ex.Message}");
            return new ApiResponse<bool>
            {
                IsSuccess = false,
                GeneralException = ex
            };
        }
    }

    public async Task<ApiResponse<User>> GetUserAsync()
    {
        try
        {
            var response = await _apiService.GetAsync<User>("auth/me");
            return new ApiResponse<User>
            {
                IsSuccess = true,
                Data = response
            };
        }
        catch (ApiException ex)
        {
            Console.WriteLine($"[AuthService] API Error: {ex.Message}");
            return new ApiResponse<User>
            {
                IsSuccess = false,
                ApiException = ex
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[AuthService] Unexpected Error: {ex.Message}");
            return new ApiResponse<User>
            {
                IsSuccess = false,
                GeneralException = ex
            };
        }
    }
}
