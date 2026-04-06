using WebApplication7.Application.DTOs;

namespace WebApplication7.Application.Interfaces.Services;

public interface ICustomerService
{
    Task<CustomerDto?> GetCustomerByIdAsync(int id);
    Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();
    Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto createCustomerDto);
    Task UpdateCustomerAsync(int id, CreateCustomerDto updateCustomerDto);
    Task DeleteCustomerAsync(int id);
}
