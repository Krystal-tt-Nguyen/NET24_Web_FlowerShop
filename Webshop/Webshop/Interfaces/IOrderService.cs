using Webshop.Shared.DTOs;

namespace Webshop.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task<OrderDto> GetByOrderIdAsync(int id);
        Task<IEnumerable<OrderDto>> GetOrderByCustomerIdAsync(int customerId);
        Task<OrderDto> CreateOrderAsync(CreateOrderDto newOrder);
    }
}
