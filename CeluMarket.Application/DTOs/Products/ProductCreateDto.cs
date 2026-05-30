using System.ComponentModel.DataAnnotations;
using CeluMarket.Domain.Enums;

namespace CeluMarket.Application.DTOs.Products;

// Lo que pedimos cuando un vendedor publica un celular
public class ProductCreateDto
{
    [Required]
    public int BrandId { get; set; }

    // NOTA: Temporalmente pediremos el SellerId por aquí hasta que implementemos el Login con JWT
    [Required]
    public Guid SellerId { get; set; }

    [Required, MaxLength(150)]
    public string Model { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Range(0.01, 100000)]
    public decimal Price { get; set; }

    [Range(1, 1000)]
    public int Stock { get; set; }

    [Required]
    public ProductCondition Condition { get; set; }
}
