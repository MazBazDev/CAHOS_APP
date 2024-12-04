namespace App.Models;

public class Session
{
    public string access_token {get; set;}
    
    public string token_type {get; set;}
    
    public Session() {}
    
    public Session(string accessToken, string tokenType)
    {
        access_token = accessToken;
        token_type = tokenType;
    }
}