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
        catch (Exception ex)
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
                return NotFound("Nao encontrado");

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
}