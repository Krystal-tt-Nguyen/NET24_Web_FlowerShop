using System.ComponentModel.DataAnnotations;

namespace Webshop.Shared.DTOs;

public class CreateProductDto
{
    [Required(ErrorMessage = "Product number is required.")]
    [StringLength(50, ErrorMessage = "Product number can't be longer than 50 characters.")]
    public string ProductNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Product name is required.")]
    [StringLength(100, ErrorMessage = "Product name can't be longer than 100 characters.")]
    public string ProductName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Description is required.")]
    [StringLength(500, ErrorMessage = "Description can't be longer than 500 characters.")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Price is required.")]
    [Range(0.01, 100000, ErrorMessage = "Price must be a positive value and cannot exceed 100 000.")]
    public decimal Price { get; set; }

    [Range(0, 10000, ErrorMessage = "Stock quantity must be a positive value and cannot exceed 10 000.")]
    public int StockQuantity { get; set; }

    [Required(ErrorMessage = "Product category ID is required.")]
    public int ProductCategoryId { get; set; }

    public bool IsDiscontinued { get; set; } = false;
}
