using CeluMarket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CeluMarket.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    // El constructor recibe las opciones (como la cadena de conexión) desde la API
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Los DbSets representan las tablas en la base de datos
    public DbSet<User> Users => Set<User>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Brand> Brands => Set<Brand>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Magia de Clean Architecture: 
        // Escanea este proyecto y aplica automáticamente cualquier clase de configuración que hayamos creado.
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}