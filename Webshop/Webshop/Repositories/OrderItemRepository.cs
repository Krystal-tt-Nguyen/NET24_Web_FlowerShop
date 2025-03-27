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

    public async Task AddOrderItemAsync(OrderItem orderItem)
    {
        var existingOrderItem = await _context.OrderItems
            .FirstOrDefaultAsync(oi => oi.OrderId == orderItem.OrderId && oi.ProductId == orderItem.ProductId);

        if (existingOrderItem is null)
        {
            _context.OrderItems.Add(orderItem);
        }
    }

    public async Task<bool> SaveChangesAsync() 
        => await _context.SaveChangesAsync() >= 0;
}