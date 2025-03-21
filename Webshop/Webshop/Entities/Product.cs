using System;
using System.Collections.Generic;

namespace Webshop.Entities;

public partial class Product
{
    public int Id { get; set; }
    public string ProductNumber { get; set; } = null!;
    public string ProductName { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int? StockQuantity { get; set; }
    public bool IsDiscontinued { get; set; }
    public int ProductCategoryId { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public virtual ProductCategory ProductCategory { get; set; } = null!;
}
