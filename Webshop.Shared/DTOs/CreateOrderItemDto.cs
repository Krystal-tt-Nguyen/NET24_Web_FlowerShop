using System.ComponentModel.DataAnnotations;

namespace Webshop.Shared.DTOs;

public class CreateOrderItemDto
{
    [Required(ErrorMessage = "Product ID is required.")]
    public int ProductId { get; set; }

    [Required(ErrorMessage = "Quantity is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
    public int Quantity { get; set; }
}
