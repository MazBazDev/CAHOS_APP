using System;
using System.Collections.Generic;

namespace App.Models;

public class Statistic
{
    public List<int> series {get; set;}
    public List<DateTime> labels {get; set;}
    
    public Statistic() {}
    
    public Statistic(List<int> series, List<DateTime> labels)
    {
        this.series = series;
        this.labels = labels;
    }
}