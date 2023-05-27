using Estoque.Data;
using Estoque.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Estoque.Controllers;
[ApiController]
public class CategoryController : ControllerBase
{
    [HttpGet("v1/categories")]
    public async Task<IActionResult> GetAsync([FromServices] AppDbContext context)
    {
        try
        {
            var categories = await context.ProductCategories.ToListAsync();
            return Ok(categories);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Falha interna no servidor");
        }
    }

    [HttpGet("v1/categories/{id:int}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id, [FromServices] AppDbContext context)
    {
        try
        {
            var category = await context.ProductCategories.FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
                return NotFound("Nao encontrado");

            return Ok(category);
        }
        catch
        {
            return StatusCode(500, "Falha interna no servidor");
        }
    }
    [HttpGet("v1/categories/{categoryId}/products")]
    public IActionResult GetProductsByCategory(int categoryId, [FromServices] AppDbContext context)
    {
        var products = context.Products
            .Where(p => p.ProductCategoryId == categoryId)
            .ToList();

        return Ok(products);
    }

    [HttpPost("v1/categories")]
    public IActionResult Post([FromBody] ProductCategory model, [FromServices] AppDbContext context)
    {
        context.ProductCategories.Add(model);
        context.SaveChanges();
            return Created($"{model.Id}", model);
    }

    [HttpPut("v1/categories/{id:int}")]
    public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] ProductCategory category,
        [FromServices] AppDbContext context)
    {
        var model = await context.ProductCategories.FirstOrDefaultAsync(x => x.Id == id);
        if (model == null)
            return NotFound();

        model.CategorieName = category.CategorieName;
        model.Slug = category.Slug;

        context.ProductCategories.Update(model);
        context.SaveChanges();
        return Ok(model);

    }

    [HttpDelete("v1/categories/{id:int}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id, [FromServices] AppDbContext context)
    {
        var model = await context.ProductCategories.FirstOrDefaultAsync(x => x.Id == id);
        if (model == null)
            return NotFound();

        context.ProductCategories.Remove(model);
        context.SaveChanges();
        return Ok(model);
    }
}