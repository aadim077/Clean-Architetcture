using Microsoft.AspNetCore.Mvc;
using WebApplication7.Application.DTOs;
using WebApplication7.Application.Interfaces.Services;

namespace WebApplication7.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAll()
    {
        var customers = await _customerService.GetAllCustomersAsync();
        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerDto>> GetById(int id)
    {
        var customer = await _customerService.GetCustomerByIdAsync(id);
        if (customer == null) return NotFound();
        
        return Ok(customer);
    }

    [HttpPost]
    public async Task<ActionResult<CustomerDto>> Create(CreateCustomerDto dto)
    {
        var createdCustomer = await _customerService.CreateCustomerAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = createdCustomer.Id }, createdCustomer);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CreateCustomerDto dto)
    {
        var existingCustomer = await _customerService.GetCustomerByIdAsync(id);
        if (existingCustomer == null) return NotFound();

        await _customerService.UpdateCustomerAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existingCustomer = await _customerService.GetCustomerByIdAsync(id);
        if (existingCustomer == null) return NotFound();

        await _customerService.DeleteCustomerAsync(id);
        return NoContent();
    }
}
