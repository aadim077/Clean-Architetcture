namespace WebApplication7.Domain.Entities;

public class OrderItem
{
    public int Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public int OrderId { get; set; }
    
    // Navigation property
    public virtual Order Order { get; set; } = null!;
}
