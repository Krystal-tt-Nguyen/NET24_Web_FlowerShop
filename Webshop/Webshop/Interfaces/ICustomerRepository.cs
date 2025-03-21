using Webshop.Entities;

namespace Webshop.Interfaces;

public interface ICustomerRepository 
{
    Task<IEnumerable<Customer>> GetCustomersAsync();
    Task<Customer?> GetCustomerByIdAsync(int id);
    Task<Customer?> GetCustomerByEmailAsync(string email);
    Task CreateCustomerAsync(Customer customer);
    void UpdateCustomer(Customer customer);
    Task<bool> SaveChangesAsync();
}
