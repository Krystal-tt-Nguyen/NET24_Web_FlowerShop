using System.ComponentModel.DataAnnotations;

namespace Webshop.DTOs;

public class CreateOrderDto
{
    [Required(ErrorMessage = "Customer ID is required.")]
    public int CustomerId { get; set; }

    [Required(ErrorMessage = "Order date is required.")]
    public DateTime OrderDate { get; set; }    

    [Required(ErrorMessage = "At least one order item is required.")]
    public IEnumerable<CreateOrderItemDto> OrderItems { get; set; } = new List<CreateOrderItemDto>();
}
