using Lab14.Data;
using Lab14.Models;
using Lab14.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab14.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvoicesController : ControllerBase
{
    private readonly AppDbContext _db;
    public InvoicesController(AppDbContext db) => _db = db;

    [HttpPost]
    public async Task<IActionResult> Insert(InvoiceInsertRequest r)
    {
        if (!await _db.Customers.AnyAsync(c => c.Id == r.IdCustomer))
            return NotFound("Cliente no encontrado");

        var invoice = new Invoice
        {
            CustomerId = r.IdCustomer,
            Date = r.Date,
            InvoiceNumber = r.InvoiceNumber,
            Total = r.Total
        };
        _db.Invoices.Add(invoice);
        await _db.SaveChangesAsync();
        return Ok(new { invoice.Id });
    }

    [HttpPost("list")]
    public async Task<IActionResult> InsertList(InvoiceListRequest r)
    {
        if (!await _db.Customers.AnyAsync(c => c.Id == r.IdCustomer))
            return NotFound("Cliente no encontrado");

        var invoices = r.Invoices.Select(i => new Invoice
        {
            CustomerId = r.IdCustomer,
            Date = i.Date,
            InvoiceNumber = i.InvoiceNumber,
            Total = i.Total
        }).ToList();

        _db.Invoices.AddRange(invoices);
        await _db.SaveChangesAsync();
        return Ok(invoices.Select(i => i.Id));
    }

    [HttpPost("details")]
    public async Task<IActionResult> InsertDetails(DetailListRequest r)
    {
        if (!await _db.Invoices.AnyAsync(i => i.Id == r.IdInvoice))
            return NotFound("Factura no encontrada");

        var ids = r.Details.Select(d => d.IdProduct).Distinct().ToList();
        if (await _db.Products.CountAsync(p => ids.Contains(p.Id)) != ids.Count)
            return NotFound("Algún producto no existe");

        var details = r.Details.Select(d => new Detail
        {
            InvoiceId = r.IdInvoice,
            ProductId = d.IdProduct,
            Amount = d.Amount,
            Price = d.Price,
            SubTotal = d.Amount * d.Price
        }).ToList();

        _db.Details.AddRange(details);
        await _db.SaveChangesAsync();
        return Ok(details.Select(d => new { d.Id, d.SubTotal }));
    }
}