using AutoMapper;
using Webshop.Shared.DTOs;
using Webshop.Entities;
using Webshop.Interfaces;

namespace Webshop.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

   public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
   {
        _customerRepository = customerRepository
            ?? throw new ArgumentNullException(nameof(customerRepository));
        _mapper = mapper
            ?? throw new ArgumentNullException(nameof(mapper));
   }

    public async Task<IEnumerable<CustomerDto>> GetAllAsync()
    {
        var customers = await _customerRepository.GetCustomersAsync();
        return _mapper.Map<IEnumerable<CustomerDto>>(customers);
    }

    public async Task<CustomerDto> GetByIdAsync(int id)
    {
        var customer = await _customerRepository.GetCustomerByIdAsync(id);
        
        if (customer is null)
        {
            return null;
        }

        return _mapper.Map<CustomerDto>(customer);
    }

    public async Task<CustomerDto> GetByEmailAsync(string email)
    {
        var customer = await _customerRepository.GetCustomerByEmailAsync(email);

        if (customer is null)
        {
            return null;
        }

        return _mapper.Map<CustomerDto>(customer);
    }

    public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto newCustomer)
    {
        var existingCustomer = await _customerRepository.GetCustomerByEmailAsync(newCustomer.Email);

        if (existingCustomer != null) // not null => email existerar redan
        {
            return null;
        }

        var customer = _mapper.Map<Customer>(newCustomer);

        await _customerRepository.CreateCustomerAsync(customer);
        await _customerRepository.SaveChangesAsync();

        return _mapper.Map<CustomerDto>(customer);
    }

    public async Task<CustomerDto> UpdateCustomerAsync(int id, UpdateCustomerDto updatedCustomer)
    {
        var existingCustomer = await _customerRepository.GetCustomerByIdAsync(id);

        if (existingCustomer is null)
        {
            return null;
        }

        _mapper.Map(updatedCustomer, existingCustomer);

        _customerRepository.UpdateCustomer(existingCustomer);

        await _customerRepository.SaveChangesAsync();

        return _mapper.Map<CustomerDto>(existingCustomer);
    }
}
