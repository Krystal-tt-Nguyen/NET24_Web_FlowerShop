using System.ComponentModel.DataAnnotations;

namespace Webshop.DTOs;

public class CreateOrderItemDto
{
    [Required(ErrorMessage = "Product ID is required.")]
    public int ProductId { get; set; }

    public int Quantity { get; set; }
}
