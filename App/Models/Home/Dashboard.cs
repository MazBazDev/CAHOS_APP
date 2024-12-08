using System.Collections.Generic;

namespace App.Models;

public class Dashboard
{
    public int total_products {get; set;}
    public int total_orders {get; set;}
    public int total_clients {get; set;}
    public List<TopSale> top_sales {get; set;}
    
    public Dashboard() {}
    
    public Dashboard(int total_products, int total_orders, int total_clients, List<TopSale> top_sales)
    {
        this.total_products = total_products;
        this.total_orders = total_orders;
        this.total_clients = total_clients;
        this.top_sales = top_sales;
    }
}

public class TopSale
{
    public string product_name {get; set;}
    public int total_sales {get; set;}
    
    public TopSale() {}
    
    public TopSale(string product_name, int total_sales)
    {
        this.product_name = product_name;
        this.total_sales = total_sales;
    }
}