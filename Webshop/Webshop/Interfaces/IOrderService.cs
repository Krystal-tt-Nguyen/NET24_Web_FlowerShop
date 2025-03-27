using Webshop.Shared.DTOs;

namespace Webshop.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> GetByOrderIdAsync(int id);
        Task<IEnumerable<OrderDto>> GetOrderByCustomerIdAsync(int customerId);
        Task<OrderDto> CreateOrderAsync(CreateOrderDto newOrder);
    }
}
