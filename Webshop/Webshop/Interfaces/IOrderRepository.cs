using Webshop.Entities;

namespace Webshop.Interfaces;

public interface IOrderRepository
{
    Task<Order?> GetOrderById(int id);
    Task<IEnumerable<Order>> GetOrdersByCustomerId(int customerId);
    Task CreateOrderAsync(Order order);
    Task<bool> SaveChangesAsync();
}

