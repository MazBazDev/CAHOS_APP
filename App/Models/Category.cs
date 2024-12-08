using System.Collections.Generic;
using System.Linq;

namespace App.Models;

public class Category
{
    public int id {get; set;}
    public string name {get; set;}
    
    public List<Product> products {get; set;}
    
    public Category() {}
    
    public Category(int id, string name, List<Product> products)
    {
        this.id = id;
        this.name = name;
        this.products = products;
    }
    
    
    public override string ToString()
    {
        return $"Category : {id} - {name} - {string.Join(", ", products?.Select(product => product.ToString()) ?? new List<string>())}";
    }
}