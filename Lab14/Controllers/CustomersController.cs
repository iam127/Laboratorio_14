using Lab14.Data;
using Lab14.Models;
using Lab14.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab14.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly AppDbContext _db;
    public CustomersController(AppDbContext db) => _db = db;

    [HttpPost]
    public async Task<IActionResult> Insert(CustomerInsertRequest r)
    {
        var customer = new Customer
        {
            FirstName = r.FirstName,
            LastName = r.LastName,
            DocumentNumber = r.DocumentNumber
        };
        _db.Customers.Add(customer);
        await _db.SaveChangesAsync();
        return Ok(new { customer.Id });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var customer = await _db.Customers.FindAsync(id);
        if (customer is null) return NotFound("Cliente no encontrado");
        if (await _db.Invoices.AnyAsync(i => i.CustomerId == id))
            return Conflict("El cliente tiene facturas asociadas");

        _db.Customers.Remove(customer);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("document")]
    public async Task<IActionResult> UpdateDocument(CustomerDocumentRequest r)
    {
        var customer = await _db.Customers.FindAsync(r.Id);
        if (customer is null) return NotFound("Cliente no encontrado");

        customer.DocumentNumber = r.DocumentNumber;
        customer.Email = r.Email;
        await _db.SaveChangesAsync();
        return NoContent();
    }
}