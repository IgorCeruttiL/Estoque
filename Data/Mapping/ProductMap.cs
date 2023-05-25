using Estoque.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estoque.Data.Mapping;

public class ProductMap : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        // Tabela
        builder.ToTable("Product");
        
        // Chave Primária
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        
        // Propriedades
        builder.Property(x => x.Name).IsRequired().HasColumnName("Name").HasColumnType("NVARCHAR").HasMaxLength(80);

        builder.Property(x => x.Description).IsRequired().HasColumnName("Description").HasColumnType("NVARCHAR")
            .HasMaxLength(200);

        builder.Property(x => x.Price).IsRequired().HasColumnName("Price").HasColumnType("Decimal");

        builder.Property(x => x.Quantity).IsRequired().HasColumnName("Quantity").HasColumnType("INT");

        builder.Property(x => x.Slug).IsRequired().HasColumnName("Slug").HasColumnType("VARCHAR").HasMaxLength(80);
        
        builder
            .HasIndex(x => x.Slug, "IX_Category_Slug")
            .IsUnique();

        builder.HasOne(x => x.ProductCategory).WithMany(x => x.Products).HasConstraintName("FK_Product_Category");
    }
}