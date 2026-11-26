using Lab14.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab14.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<Detail> Details => Set<Detail>();

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<Customer>().Property(x => x.Id).HasColumnName("idCustomers");
        mb.Entity<Product>().Property(x => x.Id).HasColumnName("idProducts");

        mb.Entity<Invoice>(e =>
        {
            e.Property(x => x.Id).HasColumnName("idInvoices");
            e.Property(x => x.CustomerId).HasColumnName("Customers_idCustomers");
            e.HasOne(x => x.Customer).WithMany(c => c.Invoices)
             .HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.Restrict);
        });

        mb.Entity<Detail>(e =>
        {
            e.Property(x => x.Id).HasColumnName("idDetails");
            e.Property(x => x.ProductId).HasColumnName("Products_idProducts");
            e.Property(x => x.InvoiceId).HasColumnName("Invoices_idInvoices");
            e.HasOne(x => x.Product).WithMany(p => p.Details)
             .HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(x => x.Invoice).WithMany(i => i.Details)
             .HasForeignKey(x => x.InvoiceId);
        });
    }
}