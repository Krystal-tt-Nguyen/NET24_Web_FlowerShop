using System.ComponentModel.DataAnnotations;

namespace Webshop.DTOs;

public class UpdateProductDto
{
    [Required(ErrorMessage = "Product number is required.")]
    [StringLength(50, ErrorMessage = "Product number can't be longer than 50 characters.")]
    public string ProductNumber { get; set; } = string.Empty;


    [Required(ErrorMessage = "Product name is required.")]
    [StringLength(100, ErrorMessage = "Product name can't be longer than 100 characters.")]
    public string ProductName { get; set; } = string.Empty;


    [StringLength(500, ErrorMessage = "Description can't be longer than 500 characters.")]
    public string Description { get; set; } = string.Empty;


    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
    public decimal Price { get; set; }


    [Range(0, int.MaxValue, ErrorMessage = "Stock quantity cannot be negative.")]
    public int StockQuantity { get; set; }


    [Required(ErrorMessage = "Product category ID is required.")]
    public int ProductCategoryId { get; set; }

    public bool IsDiscontinued { get; set; }
}
