using Juguetes.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Juguetes.Persistence.DB_Context.Configurations
{
    internal class ProductosConfiguration : IEntityTypeConfiguration<Productos>
    {
        public void Configure(EntityTypeBuilder<Productos> builder)
        {
            builder.HasKey(e => e.Id)
                    .HasName("PK_IdProducto");

            builder.ToTable("Productos");

            builder.Property(e => e.Id).HasColumnName("Id");

            builder.Property(e => e.Nombre)
                .IsRequired()
                .HasColumnName("Nombre")
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Descripcion)
                .IsRequired(false)
                .HasColumnName("Descripcion")
                .HasMaxLength(100)                
                .IsUnicode(false);

            builder.Property(e => e.RestriccionEdad).HasColumnName("RestriccionEdad");

            builder.Property(e => e.Compania)
                  .IsRequired(true)
                  .HasColumnName("Compania")
                  .HasMaxLength(50)
                  .IsUnicode(false);
        }
    }
}
