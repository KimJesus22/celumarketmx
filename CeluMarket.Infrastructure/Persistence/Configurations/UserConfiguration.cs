using CeluMarket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CeluMarket.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Nombre de la tabla explícito
        builder.ToTable("Users");

        // Llave primaria
        builder.HasKey(u => u.Id);

        // Restricciones de columnas
        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(100);

        // Índice único: No pueden existir dos usuarios con el mismo email
        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(50);
            
        // Los Enums en la base de datos se guardarán como texto (Client, Seller) en lugar de números (0, 1)
        builder.Property(u => u.Role)
            .HasConversion<string>()
            .HasMaxLength(20);
    }
}