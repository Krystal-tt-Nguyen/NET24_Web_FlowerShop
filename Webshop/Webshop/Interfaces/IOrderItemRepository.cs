using Webshop.Entities;

namespace Webshop.Interfaces;

public interface IOrderItemRepository
{
    Task<IEnumerable<OrderItem>> GetOrderItemsForOrder(int orderId);
    Task AddOrderItemAsync(OrderItem orderItem);
    Task<bool> SaveChangesAsync();
}
