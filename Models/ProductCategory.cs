namespace Estoque.Models;

public class ProductCategory
{
    public int Id { get; set; }
    public string CategorieName { get; set; }
    public string Slug { get; set; }
    public IList<Product> Products { get; set; }
}