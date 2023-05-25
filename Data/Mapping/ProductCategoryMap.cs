using Estoque.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estoque.Data.Mapping;

public class ProductCategoryMap : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        // Tabela
        builder.ToTable("ProductCategory");

        // Chave Primária
        builder.HasKey(x => x.Id);

        // Identity
        builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
        
        // Propriedades
        builder.Property(x => x.CategorieName)
            .IsRequired()
            .HasColumnName("Category")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);

        builder.Property(x => x.Slug)
            .IsRequired()
            .HasColumnName("Slug")
            .HasColumnType("VARCHAR")
            .HasMaxLength(80);
        
        // Índices
        builder.HasIndex(x => x.Slug, "IX_Category_Slug").IsUnique();
    }
}