using WebApplication7.Application.DTOs;
using WebApplication7.Application.Interfaces.Repositories;
using WebApplication7.Application.Interfaces.Services;
using WebApplication7.Domain.Entities;

namespace WebApplication7.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<CustomerDto?> GetCustomerByIdAsync(int id)
    {
        var customer = await _customerRepository.GetByIdAsync(id);
        if (customer == null) return null;

        return new CustomerDto
        {
            Id = customer.Id,
            FullName = customer.FullName,
            Email = customer.Email
        };
    }

    public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
    {
        var customers = await _customerRepository.GetAllAsync();
        return customers.Select(c => new CustomerDto
        {
            Id = c.Id,
            FullName = c.FullName,
            Email = c.Email
        });
    }

    public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto createCustomerDto)
    {
        var customer = new Customer
        {
            FullName = createCustomerDto.FullName,
            Email = createCustomerDto.Email
        };

        var createdCustomer = await _customerRepository.AddAsync(customer);

        return new CustomerDto
        {
            Id = createdCustomer.Id,
            FullName = createdCustomer.FullName,
            Email = createdCustomer.Email
        };
    }

    public async Task UpdateCustomerAsync(int id, CreateCustomerDto updateCustomerDto)
    {
        var customer = await _customerRepository.GetByIdAsync(id);
        if (customer != null)
        {
            customer.FullName = updateCustomerDto.FullName;
            customer.Email = updateCustomerDto.Email;
            await _customerRepository.UpdateAsync(customer);
        }
    }

    public async Task DeleteCustomerAsync(int id)
    {
        var customer = await _customerRepository.GetByIdAsync(id);
        if (customer != null)
        {
            await _customerRepository.DeleteAsync(customer);
        }
    }
}
