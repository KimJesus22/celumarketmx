namespace CeluMarket.Application.DTOs.Brands;

// Este es el paquete que le devolveremos al frontend (sin la lista infinita de productos)
public class BrandResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}