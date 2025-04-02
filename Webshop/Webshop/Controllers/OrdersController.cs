using Microsoft.AspNetCore.Mvc;
using Webshop.Interfaces;
using Webshop.Shared.DTOs;

namespace Webshop.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService 
            ?? throw new ArgumentNullException(nameof(orderService));
    }

    [HttpGet] 
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllOrders()
    {
        var orders = await _orderService.GetAllOrdersAsync();

        if (orders is null || !orders.Any())
        {
            return NotFound("No orders found. Please try again later.");
        }

        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDto>> GetOrderById(int id)
    {
        var order = await _orderService.GetByOrderIdAsync(id);

        return order is null 
            ? NotFound($"No order found with given ID: {id}, please try again.") 
            : Ok(order);
    }

    [HttpGet("customer/{customerId}")]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetCustomerOrders(int customerId)
    {
        var orders = await _orderService.GetOrderByCustomerIdAsync(customerId);

        if (orders is null || !orders.Any())
        {
            return NotFound($"No orders found for customer with ID: {customerId}. Please verify customer ID and try again.");
        }

        return Ok(orders);
    }

    [HttpPost]
    public async Task<ActionResult<OrderDto>> CreateOrder(CreateOrderDto newOrder)
    {       
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var order = await _orderService.CreateOrderAsync(newOrder);

        if (order is null)
        {
            return BadRequest("There was a problem creating the order. " +
                "Please verify customer ID and check order items and try again.");
        }

        return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);        
    }    
}

