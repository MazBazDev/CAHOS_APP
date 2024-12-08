using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DotNetEnv;

namespace App.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(Env.GetString("API_BASE_URL", "http://localhost:8010/api"))
        };
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    }
    
    private void AddAuthorizationHeader()
    {
        if (!string.IsNullOrEmpty(AuthService.Session?.access_token))
        {
            _httpClient.DefaultRequestHeaders.Remove("Authorization");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {AuthService.Session.access_token}");
        }
    }
    
    public async Task<T> GetAsync<T>(string url)
    {
        AddAuthorizationHeader();
        var response = await _httpClient.GetAsync(url);
        return await HandleResponse<T>(response);
    }

    public async Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest data)
    {
        AddAuthorizationHeader();
        JsonSerializerOptions opt = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
        
        var content = new StringContent(JsonSerializer.Serialize(data, opt), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content);
        return await HandleResponse<TResponse>(response);
    }

    public async Task<TResponse> PutAsync<TRequest, TResponse>(string url, TRequest data)
    {
        AddAuthorizationHeader();
        JsonSerializerOptions opt = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
        
        var content = new StringContent(JsonSerializer.Serialize(data, opt), Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync(url, content);
        return await HandleResponse<TResponse>(response);
    }
    
    // patch method
    public async Task<TResponse> PatchAsync<TRequest, TResponse>(string url, TRequest data)
    {
        AddAuthorizationHeader();
        JsonSerializerOptions opt = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
        
        var content = new StringContent(JsonSerializer.Serialize(data, opt), Encoding.UTF8, "application/json");
        var response = await _httpClient.PatchAsync(url, content);
        return await HandleResponse<TResponse>(response);
    }
    

    public async Task DeleteAsync(string url)
    {
        AddAuthorizationHeader();
        var response = await _httpClient.DeleteAsync(url);
        await HandleResponse<object>(response);
    }

    private async Task<T> HandleResponse<T>(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            try
            {
                var errorResponse = JsonSerializer.Deserialize<ApiError>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new DateTimeConverter() }
                });

                var errorMessage = errorResponse?.Message ?? "An error occurred";
                if (errorResponse?.Errors != null)
                {
                    foreach (var error in errorResponse.Errors)
                    {
                        errorMessage += $"\n - {error.Key}: {string.Join(", ", error.Value)}";
                    }
                }

                throw new ApiException(errorMessage, response.StatusCode);
            }
            catch (JsonException)
            {
                throw new ApiException($"HTTP {(int)response.StatusCode}: {content}", response.StatusCode);
            }
        }
        
        return JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true});
    }
}

public class ApiResponse<T>
{
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public ApiException? ApiException { get; set; }
    public Exception? GeneralException { get; set; }
}

public class ApiError
{
    public string Message { get; set; }
    public Dictionary<string, string[]> Errors { get; set; }
}


public class ApiException : Exception
{
    public System.Net.HttpStatusCode StatusCode { get; }

    public ApiException(string message, System.Net.HttpStatusCode statusCode) : base(message) {
        StatusCode = statusCode;
    }
}

public class DateTimeConverter : JsonConverter<DateTime>
{
    private const string Format = "yyyy-MM-ddTHH:mm:ss"; // ISO 8601 format, vous pouvez l'adapter.

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String && 
            DateTime.TryParse(reader.GetString(), out var date)) {
            return date;
        }

        throw new JsonException($"Invalid DateTime format: {reader.GetString()}");
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(Format));
    }
}