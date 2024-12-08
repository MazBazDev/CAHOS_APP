using System;

namespace App.Models;

public class Log
{
    public int id { get; set; }
    public User user { get; set; }
    public string message { get; set; }
    public string action { get; set; }
    public DateTime created_at { get; set; }
    
    public Log() {}
    
    public Log(User user, string message, string action, DateTime created_at)
    {
        this.user = user;
        this.message = message;
        this.action = action;
        this.created_at = created_at;
    }
}