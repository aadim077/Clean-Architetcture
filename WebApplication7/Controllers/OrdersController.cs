using Microsoft.AspNetCore.Mvc;
using WebApplication7.Application.DTOs;
using WebApplication7.Application.Interfaces.Services;

namespace WebApplication7.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll()
    {
        var orders = await _orderService.GetAllOrdersAsync();
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDto>> GetById(int id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);
        if (order == null) return NotFound();

        return Ok(order);
    }

    [HttpGet("customer/{customerId}")]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetByCustomerId(int customerId)
    {
        var orders = await _orderService.GetOrdersByCustomerIdAsync(customerId);
        return Ok(orders);
    }

    [HttpPost]
    public async Task<ActionResult<OrderDto>> Create(CreateOrderDto dto)
    {
        var createdOrder = await _orderService.CreateOrderAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = createdOrder.Id }, createdOrder);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existingOrder = await _orderService.GetOrderByIdAsync(id);
        if (existingOrder == null) return NotFound();

        await _orderService.DeleteOrderAsync(id);
        return NoContent();
    }
}
