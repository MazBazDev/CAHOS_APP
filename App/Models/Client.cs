using System.Collections.Generic;
using System.Linq;

namespace App.Models;

public class Client
{
    public int id {get; set;}
    public string name {get; set;}
    public string address {get; set;}
    public string siret {get; set;}
    
    public List<Order>? orders {get; set;}
    
    public Client() {}
    
    public Client(int id, string name, string address, string siret, List<Order> orders)
    {
        this.id = id;
        this.name = name;
        this.address = address;
        this.siret = siret;
        this.orders = orders;
    }
    
    public override string ToString()
    {
        return $"Client: {name} - Address: {address} - Siret: {siret} - Orders: {string.Join(", ", orders?.Select(order => order.ToString()) ?? new List<string>())}";
    }
}