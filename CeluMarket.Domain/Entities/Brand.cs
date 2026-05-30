namespace CeluMarket.Domain.Entities;

public class Brand
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    // Propiedad de navegación para relacionar la marca con sus celulares
    public ICollection<Product> Products { get; set; } = new List<Product>();
}