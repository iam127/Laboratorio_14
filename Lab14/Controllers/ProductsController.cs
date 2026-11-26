using Lab14.Data;
using Lab14.Models;
using Lab14.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab14.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _db;
    public ProductsController(AppDbContext db) => _db = db;

    [HttpPost]
    public async Task<IActionResult> Insert(ProductInsertRequest r)
    {
        var product = new Product { Name = r.Name, Price = r.Price };
        _db.Products.Add(product);
        await _db.SaveChangesAsync();
        return Ok(new { product.Id, product.Name, product.Price });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _db.Products.FindAsync(id);
        if (product is null) return NotFound("Producto no encontrado");
        if (await _db.Details.AnyAsync(d => d.ProductId == id))
            return Conflict("El producto tiene detalles asociados");

        _db.Products.Remove(product);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("price")]
    public async Task<IActionResult> UpdatePrice(ProductPriceRequest r)
    {
        var product = await _db.Products.FindAsync(r.Id);
        if (product is null) return NotFound("Producto no encontrado");

        product.Price = r.Price;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("list")]
    public async Task<IActionResult> DeleteList([FromBody] List<IdRequest> r)
    {
        var ids = r.Select(x => x.Id).Distinct().ToList();
        if (ids.Count == 0) return BadRequest("La lista está vacía");

        var products = await _db.Products.Where(p => ids.Contains(p.Id)).ToListAsync();
        if (products.Count != ids.Count) return NotFound("Algún producto no existe");
        if (await _db.Details.AnyAsync(d => ids.Contains(d.ProductId)))
            return Conflict("Algún producto tiene detalles asociados");

        _db.Products.RemoveRange(products);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}