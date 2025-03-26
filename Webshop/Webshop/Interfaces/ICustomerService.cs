using Webshop.DTOs;

namespace Webshop.Interfaces;

public interface ICustomerService
{
    Task<IEnumerable<CustomerDto>> GetAllAsync();
    Task<CustomerDto> GetByIdAsync(int id);
    Task<CustomerDto> GetByEmailAsync(string email);
    Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto newCustomer);
    Task<CustomerDto> UpdateCustomerAsync(int id, UpdateCustomerDto updatedCustomer);
}
