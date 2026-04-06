using Microsoft.EntityFrameworkCore;
using WebApplication7.Application.Interfaces.Repositories;
using WebApplication7.Domain.Entities;
using WebApplication7.Infrastructure.Data;

namespace WebApplication7.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _dbContext;

    public OrderRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
        return await _dbContext.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await _dbContext.Orders
            .Include(o => o.OrderItems)
            .ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetByCustomerIdAsync(int customerId)
    {
        return await _dbContext.Orders
            .Include(o => o.OrderItems)
            .Where(o => o.CustomerId == customerId)
            .ToListAsync();
    }

    public async Task<Order> AddAsync(Order order)
    {
        await _dbContext.Orders.AddAsync(order);
        await _dbContext.SaveChangesAsync();
        return order;
    }

    public async Task UpdateAsync(Order order)
    {
        _dbContext.Orders.Update(order);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Order order)
    {
        _dbContext.Orders.Remove(order);
        await _dbContext.SaveChangesAsync();
    }
}
