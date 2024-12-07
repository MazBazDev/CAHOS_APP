using System.Collections.Generic;

namespace App.Models;

public class Alert
{
    public List<string> stocks {get; set;}
    public List<string> expire_soon {get; set;}
    
    public Alert() {}
    
    public Alert(List<string> stocks, List<string> expire_soon)
    {
        this.stocks = stocks;
        this.expire_soon = expire_soon;
    }
}