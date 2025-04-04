using Microsoft.AspNetCore.Mvc;
using Webshop.Interfaces;
using Webshop.Shared.DTOs;

namespace Webshop.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService 
            ?? throw new ArgumentNullException(nameof(customerService));
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomers()
    {
        var customers = await _customerService.GetAllAsync();
        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerDto>> GetCustomerById(int id)
    {
        var customer = await _customerService.GetByIdAsync(id);

        if (customer is null)
        {
            return NotFound($"No customer found with given ID: {id}.");
        }

        return Ok(customer);
    }

    [HttpGet("email/{email}")]
    public async Task<ActionResult<CustomerDto>> GetCustomerByEmail(string email)
    {
        var customer = await _customerService.GetByEmailAsync(email);

        if (customer is null)
        {
            return NotFound($"No customer found with given email address: {email}.");
        }

        return Ok(customer);
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
        
        var customer = await _customerService.CreateCustomerAsync(newCustomer);

        if (customer is null)
        {
            return Conflict("The email address provided is already registered. Please use a different email address.");
        }

        return CreatedAtAction(
            nameof(GetCustomerById),      // Refererar till en annan metod i controllern, vanligtvis en GET-metod
            new { id = customer?.Id },    // Den nya kundens id som URL-parameter
            customer);                    // Data som returneras        
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

        var customer = await _customerService.UpdateCustomerAsync(id, updatedCustomer);

        if (customer is null)
        {
            return NotFound($"No customer found with given ID: {id}.");
        }

        return Ok(customer);                
    }
}