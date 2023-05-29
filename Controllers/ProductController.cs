using Estoque.Data;
using Estoque.Models;
using Estoque.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Estoque.Controllers;

[ApiController]
public class ProductController : ControllerBase
{
    [HttpGet("v1/products")]
    public async Task<IActionResult> GetAsync([FromServices] AppDbContext context)
    {
        try
        {
            var products = await context.Products.ToListAsync();
            return Ok(products);
        }
        catch (Exception)
        {
            return StatusCode(500, "Falha interna no servidor");
        }
    }

    [HttpGet("v1/products/{id:int}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id, [FromServices] AppDbContext context)
    {
        try
        {
            var product = await context.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
                return NotFound("Não encontrado");

            return Ok(product);
        }
        catch       
        {
            return StatusCode(500, "Falha interna no servidor");
        }
    }

    [HttpPost("v1/products")]
    public async Task<IActionResult> PostAsync([FromBody] EditorProductViewModel model, [FromServices] AppDbContext context)
    {
        var product = new Product
        {
            Name = model.Name,
            Description = model.Description,
            Price = model.Price,
            Quantity = model.Quantity,
            Slug = model.Slug,
            ProductCategoryId = model.ProductCategoryId
            
        };
        
        context.Products.Add(product);
        await context.SaveChangesAsync();
        return Created($"{product.Id}", product);
    }

    [HttpPut("v1/products/{id:int}")]
    public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] EditorProductViewModel model,
        [FromServices] AppDbContext context)
    {
        var products = await context.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (products == null)
            return NotFound();

        products.Name = model.Name;
        products.Description = model.Description;
        products.Slug = model.Slug;
        
        await context.SaveChangesAsync();
        return Ok(products);
    }

    [HttpDelete("v1/products/{id:int}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id, [FromServices] AppDbContext context)
    {
        var model = await context.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (model == null)
            return NotFound();

        context.Products.Remove(model);
        await context.SaveChangesAsync();
        return Ok(model);
    }
}