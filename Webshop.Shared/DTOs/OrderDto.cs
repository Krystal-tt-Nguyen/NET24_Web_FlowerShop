namespace Webshop.Shared.DTOs;

public class OrderDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public string OrderStatus { get; set; } = "Pending";
    public CustomerDto Customer { get; set; } = new CustomerDto(); // inkludera kundinfo för varje order
    public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>(); // inkludera info över orderrader för varje order
}
