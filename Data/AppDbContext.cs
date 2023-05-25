using Estoque.Data.Mapping;
using Estoque.Data.Mapping;
using Estoque.Models;
using Microsoft.EntityFrameworkCore;

namespace Estoque.Data;

public class AppDbContext : DbContext
{
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer("Server=localhost,1433;DataBase=Estoque;User=sa;Password=1q2w3e4r@#$;TrustServerCertificate=True");  

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductCategoryMap());
        modelBuilder.ApplyConfiguration(new ProductMap());
    }
}