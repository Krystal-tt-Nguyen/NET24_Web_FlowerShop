using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Webshop.Entities;
using Webshop.Interfaces;

namespace Webshop.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly FlowerboutiqueContext _context;

    public CustomerRepository(FlowerboutiqueContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async Task<IEnumerable<Customer>> GetCustomersAsync() 
        => await _context.Customers.ToListAsync();
 
    public async Task<Customer?> GetCustomerByIdAsync(int id)
        => await _context.Customers.FindAsync(id);
    
    public async Task<Customer?> GetCustomerByEmailAsync(string email) 
        => await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);

    public async Task CreateCustomerAsync(Customer customer) 
        => await _context.Customers.AddAsync(customer);

    public void UpdateCustomer(Customer customer) 
        => _context.Customers.Update(customer);

    public async Task<bool> SaveChangesAsync() 
        => await _context.SaveChangesAsync() >= 0;

}
