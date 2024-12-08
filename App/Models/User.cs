using System;

namespace App.Models;

public class User
{
    public int id { get; set; }
    public string name { get; set; }
    public string email { get; set; }
    
    public DateTime? email_verified_at { get; set; }
    public DateTime Created_at { get; set; }
    public DateTime Updated_at { get; set; }
    
    public User() {}
    
    public User(string name, string email, DateTime? email_verified_at, DateTime Created_at, DateTime Updated_at)
    {
        this.name = name;
        this.email = email;
        this.email_verified_at = email_verified_at;
        this.Created_at = Created_at;
        this.Updated_at = Updated_at;
    }
}