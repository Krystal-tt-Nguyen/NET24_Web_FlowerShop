namespace Webshop.Shared.DTOs;

public class ProductDto
{
    public int Id { get; set; }
    public string ProductNumber { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int? StockQuantity { get; set; }
    public int ProductCategoryId { get; set; }
    public string? ProductCategoryName { get; set; }
    public bool IsDiscontinued { get; set; }
}
