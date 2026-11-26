using System.ComponentModel.DataAnnotations;

namespace Lab14.Models;

public class Customer
{
    public int Id { get; set; }
    [MaxLength(45)] public string FirstName { get; set; } = "";
    [MaxLength(45)] public string LastName { get; set; } = "";
    [MaxLength(45)] public string DocumentNumber { get; set; } = "";
    [MaxLength(100)] public string? Email { get; set; }
    public List<Invoice> Invoices { get; set; } = new();
}