namespace App.Models;

public class Product
{
    public int id {get; set;}
    public string name {get; set;}
    public int quantity {get; set;}
    public double price {get; set;}
    public string expiration_date {get; set;}
    public string location {get; set;}
    public Category? category {get; set;}
    public int? category_id {get; set;}
    public string created_at {get; set;}
    public string updated_at {get; set;}
    
    public Product() {}
    
    public Product(int id, string name, int quantity, double price, string expirationDate, string location, string createdAt, string updatedAt, Category category)
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
}