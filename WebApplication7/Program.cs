using Microsoft.EntityFrameworkCore;
using WebApplication7.Application.Interfaces.Repositories;
using WebApplication7.Application.Interfaces.Services;
using WebApplication7.Application.Services;
using WebApplication7.Infrastructure.Data;
using WebApplication7.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure EF Core with PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
    b => b.MigrationsAssembly("WebApplication7.Infrastructure")));

// Dependency Injection for Repositories
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// Dependency Injection for Services
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IOrderService, OrderService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Disable HTTPS redirection warning for local HTTP testing
// app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
