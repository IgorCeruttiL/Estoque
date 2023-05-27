namespace Estoque.ViewModels;

public class EditorProductViewModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
    public string Slug { get; set; }
    public int ProductCategoryId { get; set; }
}