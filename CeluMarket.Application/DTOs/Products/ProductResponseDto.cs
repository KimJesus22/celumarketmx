namespace CeluMarket.Application.DTOs.Products;

// Lo que verá el cliente en la tienda
public class ProductResponseDto
{
    public Guid Id { get; set; }
    public string BrandName { get; set; } = string.Empty; // En lugar del ID, mandamos el nombre de la marca
    public string Model { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Condition { get; set; } = string.Empty;
    public int Stock { get; set; }
    public DateTime CreatedAt { get; set; }
}
