namespace App.Models;

public class Dashboard
{
    public int total_products {get; set;}
    public int total_orders {get; set;}
    public int total_clients {get; set;}
    
    public Dashboard() {}
    
    public Dashboard(int total_products, int total_orders, int total_clients)
    {
        this.total_products = total_products;
        this.total_orders = total_orders;
        this.total_clients = total_clients;
    }
}