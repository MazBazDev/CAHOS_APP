using System;

namespace App.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    
    public DateTime? EmailVerifiedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public User() {}
    
    public User(int id, string name, string email, DateTime? emailVerifiedAt, DateTime createdAt, DateTime updatedAt)
    {
        Id = id;
        Name = name;
        Email = email;
        EmailVerifiedAt = emailVerifiedAt;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}