using WebApplication7.Application.DTOs;
using WebApplication7.Application.Interfaces.Repositories;
using WebApplication7.Application.Interfaces.Services;
using WebApplication7.Domain.Entities;

namespace WebApplication7.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OrderDto?> GetOrderByIdAsync(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null) return null;

        return MapToDto(order);
    }

    public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
    {
        var orders = await _orderRepository.GetAllAsync();
        return orders.Select(MapToDto);
    }

    public async Task<IEnumerable<OrderDto>> GetOrdersByCustomerIdAsync(int customerId)
    {
        var orders = await _orderRepository.GetByCustomerIdAsync(customerId);
        return orders.Select(MapToDto);
    }

    public async Task<OrderDto> CreateOrderAsync(CreateOrderDto callDto)
    {
        var orderItems = callDto.OrderItems.Select(oi => new OrderItem
        {
            ProductName = oi.ProductName,
            Quantity = oi.Quantity,
            UnitPrice = oi.UnitPrice
        }).ToList();

        var order = new Order
        {
            CustomerId = callDto.CustomerId,
            OrderDate = callDto.OrderDate,
            TotalAmount = orderItems.Sum(oi => oi.Quantity * oi.UnitPrice),
            OrderItems = orderItems
        };

        var createdOrder = await _orderRepository.AddAsync(order);

        return MapToDto(createdOrder);
    }

    public async Task DeleteOrderAsync(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order != null)
        {
            await _orderRepository.DeleteAsync(order);
        }
    }

    private static OrderDto MapToDto(Order order)
    {
        return new OrderDto
        {
            Id = order.Id,
            CustomerId = order.CustomerId,
            OrderDate = order.OrderDate,
            TotalAmount = order.TotalAmount,
            OrderItems = order.OrderItems.Select(oi => new OrderItemDto
            {
                Id = oi.Id,
                ProductName = oi.ProductName,
                Quantity = oi.Quantity,
                UnitPrice = oi.UnitPrice
            }).ToList()
        };
    }
}
