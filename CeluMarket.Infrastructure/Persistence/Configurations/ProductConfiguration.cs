using CeluMarket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CeluMarket.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Model)
            .IsRequired()
            .HasMaxLength(150);

        // Tipo de dato exacto para dinero en MariaDB, evitando errores de redondeo flotante
        builder.Property(p => p.Price)
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.Condition)
            .HasConversion<string>()
            .HasMaxLength(20);

        // Definir la relación con Brand (1 a Muchos)
        builder.HasOne(p => p.Brand)
            .WithMany(b => b.Products)
            .HasForeignKey(p => p.BrandId)
            .OnDelete(DeleteBehavior.Restrict); // Si se borra una marca, no se borran sus celulares

        // Definir la relación con Seller (1 a Muchos)
        builder.HasOne(p => p.Seller)
            .WithMany()
            .HasForeignKey(p => p.SellerId)
            .OnDelete(DeleteBehavior.Cascade); // Si se borra un vendedor, se borran sus publicaciones
    }
}