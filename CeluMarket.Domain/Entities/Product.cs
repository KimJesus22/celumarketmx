using CeluMarket.Domain.Enums;

namespace CeluMarket.Domain.Entities;

public class Product
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid SellerId { get; set; }
    public int BrandId { get; set; }
    
    public string Model { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public ProductCondition Condition { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;

    // Propiedades de navegación (El símbolo '?' indica que puede ser nulo en memoria temporalmente)
    public User? Seller { get; set; }
    public Brand? Brand { get; set; }
}