using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Models;

public class Client
{
    public int id {get; set;}
    public string name {get; set;}
    public string address {get; set;}
    public string siret {get; set;}
    
    public DateTime created_at {get; set;}
    public DateTime updated_at {get; set;}
    
    public List<Order?>? orders {get; set;} = new List<Order>();
    
    public Client() {}
    
    public Client(int id, string name, string address, string siret, DateTime createdAt, DateTime updatedAt, List<Order> orders)
    {
        this.id = id;
        this.name = name;
        this.address = address;
        this.siret = siret;
        this.orders = orders;
        this.created_at = createdAt;
        this.updated_at = updatedAt;
    }
    
    public override string ToString()
    {
        return $"Client: {name} - Address: {address} - Siret: {siret} - Created_at : {created_at} - Updated_at : {updated_at} - Orders: {string.Join(", ", orders?.Select(order => order.ToString()) ?? new List<string>())}";
    }
}