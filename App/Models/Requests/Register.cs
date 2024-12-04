using System;
using App.Services;

namespace App.Models.Requests;

public class RegisterRequest
{
    public string name { get; set; }
    public string email { get; set; }
    public string password { get; set; }
}

public class RegisterResponse
{
    public string access_token { get; set; }
    public string token_type { get; set; }
}