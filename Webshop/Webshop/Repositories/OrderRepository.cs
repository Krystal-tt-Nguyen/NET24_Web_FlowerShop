using Microsoft.EntityFrameworkCore;
using Webshop.Entities;
using Webshop.Interfaces;

namespace Webshop.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly FlowerboutiqueContext _context;

    public OrderRepository(FlowerboutiqueContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Order?> GetOrderById(int id)
        => await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);    

    public async Task<IEnumerable<Order>> GetOrdersByCustomerId(int customerId)
        => await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.CustomerId == customerId)
                .ToListAsync();

    public async Task CreateOrderAsync(Order order) 
        => await _context.Orders.AddAsync(order);

    public async Task<bool> SaveChangesAsync() 
        => await _context.SaveChangesAsync() >= 0;
}

