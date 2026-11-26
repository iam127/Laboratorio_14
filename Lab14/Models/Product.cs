using System.ComponentModel.DataAnnotations;

namespace Lab14.Models;

public class Product
{
    public int Id { get; set; }
    [MaxLength(45)] public string Name { get; set; } = "";
    public double Price { get; set; }
    public List<Detail> Details { get; set; } = new();
}