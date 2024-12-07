using System;

namespace App.Models;

public class Order
{
    public int id {get; set;}
    public Client? client {get; set;}
    public int? client_id { get; set; }
    
    public Product? product {get; set;}
    public int? product_id { get; set; }
    public int quantity {get; set;}
    public int total {get; set;}
    public string? status {get; set;}
    public DateTime? order_date {get; set;}
    
    public Order() {}
    
    public Order(int client_id, int product_id ,int quantity, string? status = null, DateTime? order_date = null)
    {
        this.client_id = client_id;
        this.product_id = product_id;
        this.quantity = quantity;
        this.status = status;
        this.order_date = order_date;
    }
    
    public override string ToString()
    {
        return $"Order: {id} - Client: {client?.name} - Product: {product?.name} - Quantity: {quantity} - Total: {total} - Status: {status} - Order Date: {order_date}";
    }
}