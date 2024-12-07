using System;

namespace App.Models;

public class Statistic
{
    public DateTime date {get; set;}
    public int total {get; set;}
    
    public Statistic() {}
    
    public Statistic(DateTime date, int total)
    {
        this.date = date;
        this.total = total;
    }
}