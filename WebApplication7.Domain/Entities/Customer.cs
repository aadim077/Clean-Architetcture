namespace WebApplication7.Domain.Entities;

public class Customer
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    // Navigation property
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
