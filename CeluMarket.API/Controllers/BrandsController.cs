using CeluMarket.Application.DTOs.Brands;
using CeluMarket.Domain.Entities;
using CeluMarket.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CeluMarket.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BrandsController : ControllerBase
{
    private readonly AppDbContext _context;

    public BrandsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/brands
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BrandResponseDto>>> GetBrands()
    {
        // Traemos de la BD y mapeamos manualmente al DTO
        var brands = await _context.Brands
            .Select(b => new BrandResponseDto 
            { 
                Id = b.Id, 
                Name = b.Name 
            })
            .ToListAsync();

        return Ok(brands);
    }

    // POST: api/brands
    [HttpPost]
    public async Task<ActionResult<BrandResponseDto>> CreateBrand(BrandCreateDto dto)
    {
        // 1. Transformar el DTO de entrada a Entidad de Dominio
        var brand = new Brand
        {
            Name = dto.Name
        };

        _context.Brands.Add(brand);
        await _context.SaveChangesAsync();

        // 2. Transformar la Entidad recién creada al DTO de salida
        var responseDto = new BrandResponseDto
        {
            Id = brand.Id,
            Name = brand.Name
        };

        // Devolvemos el DTO limpio
        return CreatedAtAction(nameof(GetBrands), new { id = brand.Id }, responseDto);
    }
}