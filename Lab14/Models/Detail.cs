namespace Lab14.Models;

public class Detail
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public int InvoiceId { get; set; }
    public Invoice? Invoice { get; set; }
    public int Amount { get; set; }
    public double Price { get; set; }
    public double SubTotal { get; set; }
}