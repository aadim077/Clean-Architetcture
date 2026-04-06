using WebApplication7.Application.DTOs;

namespace WebApplication7.Application.Interfaces.Services;

public interface IOrderService
{
    Task<OrderDto?> GetOrderByIdAsync(int id);
    Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
    Task<IEnumerable<OrderDto>> GetOrdersByCustomerIdAsync(int customerId);
    Task<OrderDto> CreateOrderAsync(CreateOrderDto callDto);
    Task DeleteOrderAsync(int id);
}
