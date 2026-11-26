using System.ComponentModel.DataAnnotations;

namespace Lab14.Models;

public class Invoice
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public DateTime Date { get; set; }
    [MaxLength(45)] public string InvoiceNumber { get; set; } = "";
    public double Total { get; set; }
    public List<Detail> Details { get; set; } = new();
}