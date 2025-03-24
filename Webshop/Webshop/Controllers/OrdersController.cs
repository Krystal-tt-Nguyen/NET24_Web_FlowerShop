using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Webshop.DTOs;
using Webshop.Entities;
using Webshop.Interfaces;

namespace Webshop.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public OrdersController(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, 
        ICustomerRepository customerRepository, IProductRepository productRepository, IMapper mapper)
    {
        _orderRepository = orderRepository
            ?? throw new ArgumentNullException(nameof(orderRepository));
        _orderItemRepository = orderItemRepository
            ?? throw new ArgumentNullException(nameof(orderItemRepository));
        _customerRepository = customerRepository
           ?? throw new ArgumentNullException(nameof(customerRepository));
        _productRepository = productRepository
           ?? throw new ArgumentNullException(nameof(productRepository));
        _mapper = mapper
            ?? throw new ArgumentNullException(nameof(mapper));
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDto>> GetOrderById(int id)
    {
        var order = await _orderRepository.GetOrderById(id);

        return order is null 
            ? NotFound($"No order found with given ID: {id}, please try again.") 
            : Ok(_mapper.Map<OrderDto>(order));
    }

    [HttpGet("customer/{customerId}")]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetCustomerOrders(int customerId)
    {
        var customer = await _customerRepository.GetCustomerByIdAsync(customerId);

        if (customer is null)
        {
            return NotFound($"No customer found with given ID: {customerId}, please try again.");
        }

        var orders = await _orderRepository.GetOrdersByCustomerId(customerId);

        return orders is null || !orders.Any()
            ? NotFound($"No orders found for the customer with ID: {customerId}. Please verify customer ID and try again.")
            : Ok(_mapper.Map<IEnumerable<OrderDto>>(orders));
    }

    [HttpPost]
    public async Task<ActionResult<CreateOrderDto>> CreateOrder(CreateOrderDto newOrder)
    {
        if (newOrder.OrderItems == null || !newOrder.OrderItems.Any())
        {
            return BadRequest("Order must contain at least one item.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var existingCustomer = await _customerRepository.GetCustomerByIdAsync(newOrder.CustomerId);

            if (existingCustomer is null)
            {
                return NotFound($"Unable to create order. An order must be associated with an existing customer.");
            }

            var order = _mapper.Map<Order>(newOrder);

            await _orderRepository.CreateOrderAsync(order);

            // Sparar ordern så att den får ett ID innan vi lägger till OrderItems
            await _orderRepository.SaveChangesAsync();

            var orderItems = _mapper.Map<IEnumerable<OrderItem>>(newOrder.OrderItems);

            foreach (var orderItem in orderItems)
            {
                var productExists = await _productRepository.GetProductByIdAsync(orderItem.ProductId);

                if (productExists is null)
                {
                    return BadRequest($"Product with ID {orderItem.ProductId} does not exist.");
                }

                orderItem.OrderId = order.Id; // Koppla orderItem till den nya ordern
               
                await _orderItemRepository.AddOrderItemAsync(orderItem); 
            }

            await _orderItemRepository.SaveChangesAsync(); 

            var orderDto = _mapper.Map<OrderDto>(order);

            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, orderDto);
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }    
}

