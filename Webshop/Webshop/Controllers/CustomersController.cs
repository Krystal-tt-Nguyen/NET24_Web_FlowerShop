using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Webshop.DTOs;
using Webshop.Entities;
using Webshop.Interfaces;

namespace Webshop.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public CustomersController(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository
            ?? throw new ArgumentNullException(nameof(customerRepository));
        _mapper = mapper
            ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomers()
    {
        var customers = await _customerRepository.GetCustomersAsync();
        return Ok(_mapper.Map<IEnumerable<CustomerDto>>(customers));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerDto>> GetCustomerById(int id)
    {
        var customer = await _customerRepository.GetCustomerByIdAsync(id);

        if (customer is null)
        {
            return NotFound($"No customer found with given ID: {id}, please try again.");
        }

        return Ok(_mapper.Map<CustomerDto>(customer));
    }

    [HttpGet("email/{email}")]
    public async Task<ActionResult<CustomerDto>> GetCustomerByEmail(string email)
    {
        var customer = await _customerRepository.GetCustomerByEmailAsync(email);

        if (customer is null)
        {
            return NotFound($"No customer found with given email address: {email}, please try again.");
        }

        return Ok(_mapper.Map<CustomerDto>(customer));
    }

    [HttpPost]
    public async Task<ActionResult<CustomerDto>> CreateCustomer(CreateCustomerDto newCustomer)
    {
        if (newCustomer is null)
        {
            return BadRequest("Customer data cannot be null.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var existingCustomer = await _customerRepository.GetCustomerByEmailAsync(newCustomer.Email);

            if (existingCustomer != null)
            {
                return Conflict("The email address provided is already registered. Please use a different email address.");
            }

            var customer = _mapper.Map<Customer>(newCustomer);

            await _customerRepository.CreateCustomerAsync(customer);
            await _customerRepository.SaveChangesAsync();

            var customerDto = _mapper.Map<CustomerDto>(customer);

            return CreatedAtAction(
                nameof(GetCustomerById),  // Refererar till en annan metod i controllern, vanligtvis en GET-metod
                new { id = customer.Id }, // Den nya kundens id som URL-parameter
                customerDto               // Data som returneras
            );
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CustomerDto>> UpdateCustomer(int id, UpdateCustomerDto updatedCustomer)
    {
        if (updatedCustomer is null)
        {
            return BadRequest("Customer data cannot be null.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var existingCustomer = await _customerRepository.GetCustomerByIdAsync(id);

            if (existingCustomer is null)
            {
                return NotFound($"No customer found with given ID: {id}, please try again.");
            }

            _mapper.Map(updatedCustomer, existingCustomer);

            _customerRepository.UpdateCustomer(existingCustomer);
            
            await _customerRepository.SaveChangesAsync();

            return Ok(_mapper.Map<CustomerDto>(existingCustomer));
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }
}