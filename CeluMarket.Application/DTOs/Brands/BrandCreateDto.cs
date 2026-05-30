using System.ComponentModel.DataAnnotations;

namespace CeluMarket.Application.DTOs.Brands;

// Este es el paquete que le exigiremos al frontend cuando quiera crear una marca
public class BrandCreateDto
{
    [Required(ErrorMessage = "El nombre de la marca es obligatorio.")]
    [MinLength(2, ErrorMessage = "El nombre debe tener al menos 2 caracteres.")]
    public string Name { get; set; } = string.Empty;
}