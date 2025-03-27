using AutoMapper;
using Webshop.Entities;
using Webshop.Interfaces;
using Webshop.Shared.DTOs;

namespace Webshop.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository,
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
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<OrderDto> GetByOrderIdAsync(int id)
    {
        var order = await _orderRepository.GetOrderById(id);
        
        if (order is null)
        {
            return null;
        }

        return _mapper.Map<OrderDto>(order);
    }

    public async Task<IEnumerable<OrderDto>> GetOrderByCustomerIdAsync(int customerId)
    {
        var customer = await _customerRepository.GetCustomerByIdAsync(customerId);

        if (customer is null)
        {
            return null;
        }

        var orders = await _orderRepository.GetOrdersByCustomerId(customerId);

        return _mapper.Map<IEnumerable<OrderDto>>(orders);
    }

    public async Task<OrderDto> CreateOrderAsync(CreateOrderDto newOrder)
    {
        if (newOrder.OrderItems == null || !newOrder.OrderItems.Any())
        {
            return null;
        }

        var existingCustomer = await _customerRepository.GetCustomerByIdAsync(newOrder.CustomerId);

        if (existingCustomer is null)
        {
            return null;
        }

        var order = _mapper.Map<Order>(newOrder);

        await _orderRepository.CreateOrderAsync(order);
        await _orderRepository.SaveChangesAsync();

        var orderItems = _mapper.Map<IEnumerable<OrderItem>>(newOrder.OrderItems);

        foreach (var orderItem in orderItems)
        {
            var productExists = await _productRepository.GetProductByIdAsync(orderItem.ProductId);

            if (productExists is null)
            {
                return null;
            }

            orderItem.OrderId = order.Id;

            await _orderItemRepository.AddOrderItemAsync(orderItem);
        }

        await _orderItemRepository.SaveChangesAsync();

        return _mapper.Map<OrderDto>(order);
    }
}
