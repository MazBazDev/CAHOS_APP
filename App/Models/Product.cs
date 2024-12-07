using System;

namespace App.Models;

public class Product
{
    public int id {get; set;}
    public string name {get; set;}
    public int? quantity {get; set;}
    public decimal? price {get; set;}
    public DateTime expiration_date {get; set;}
    public string location {get; set;}
    public Category? category {get; set;}
    public int? category_id {get; set;}
    public DateTime created_at {get; set;}
    public DateTime updated_at {get; set;}
    
    public Product() {}
    
    public Product(int id, string name, DateTime expirationDate, string location, DateTime createdAt, DateTime updatedAt, Category category, int? quantity = null, decimal? price = null)
    {
       this.id = id;
       this.name = name;
       this.quantity = quantity;
       this.price = price;
       this.expiration_date = expirationDate;
       this.location = location;
       this.created_at = createdAt;
       this.updated_at = updatedAt;
       this.category = category;
    }
    
    public override string ToString()
    {
        return $"Product: {name} - {quantity} - {price} - {expiration_date} - {location} - {category_id}";
    }
}