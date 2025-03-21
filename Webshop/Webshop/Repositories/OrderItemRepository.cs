using Microsoft.EntityFrameworkCore;
using Webshop.Entities;
using Webshop.Interfaces;

namespace Webshop.Repositories;

public class OrderItemRepository : IOrderItemRepository
{
    private readonly FlowerboutiqueContext _context;

    public OrderItemRepository(FlowerboutiqueContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<OrderItem>> GetOrderItemsForOrder(int orderId) 
        => await _context.OrderItems.Include(o => o.Product).Where(o => o.OrderId == orderId).ToListAsync();
    
    public async Task AddOrderItemAsync(OrderItem orderItem) => await _context.OrderItems.AddAsync(orderItem);
    
    public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() >= 0;
}