using Microsoft.EntityFrameworkCore;
using WebApplication7.Application.Interfaces.Repositories;
using WebApplication7.Domain.Entities;
using WebApplication7.Infrastructure.Data;

namespace WebApplication7.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CustomerRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Customer?> GetByIdAsync(int id)
    {
        return await _dbContext.Customers.FindAsync(id);
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _dbContext.Customers.ToListAsync();
    }

    public async Task<Customer> AddAsync(Customer customer)
    {
        await _dbContext.Customers.AddAsync(customer);
        await _dbContext.SaveChangesAsync();
        return customer;
    }

    public async Task UpdateAsync(Customer customer)
    {
        _dbContext.Customers.Update(customer);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Customer customer)
    {
        _dbContext.Customers.Remove(customer);
        await _dbContext.SaveChangesAsync();
    }
}
