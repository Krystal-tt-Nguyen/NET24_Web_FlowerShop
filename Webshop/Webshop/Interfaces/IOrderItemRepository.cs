using Webshop.Entities;

namespace Webshop.Interfaces;

public interface IOrderItemRepository
{
    Task AddOrderItemAsync(OrderItem orderItem);
    Task<bool> SaveChangesAsync();
}
