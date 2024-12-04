namespace App.Models.Requests;

public class LoginRequest
{
    public string email { get; set; }
    public string password { get; set; }
}

public class LoginResponse
{
    public string access_token { get; set; }
    public string token_type { get; set; }
}