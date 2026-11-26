using System.ComponentModel.DataAnnotations;

namespace Lab14.Requests;

public record ProductInsertRequest([Required, MaxLength(45)] string Name, [Range(0.01, double.MaxValue)] double Price);
public record ProductPriceRequest(int Id, [Range(0.01, double.MaxValue)] double Price);
public record IdRequest(int Id);

public record CustomerInsertRequest(
    [Required, MaxLength(45)] string FirstName,
    [Required, MaxLength(45)] string LastName,
    [Required, MaxLength(45)] string DocumentNumber);

public record CustomerDocumentRequest(
    int Id,
    [Required, MaxLength(45)] string DocumentNumber,
    [Required, EmailAddress, MaxLength(100)] string Email);

public record InvoiceInsertRequest(
    int IdCustomer,
    DateTime Date,
    [Required, MaxLength(45)] string InvoiceNumber,
    [Range(0, double.MaxValue)] double Total);

public record InvoiceItemRequest(
    DateTime Date,
    [Required, MaxLength(45)] string InvoiceNumber,
    [Range(0, double.MaxValue)] double Total);

public record InvoiceListRequest(int IdCustomer, [Required, MinLength(1)] List<InvoiceItemRequest> Invoices);

public record DetailItemRequest(
    int IdProduct,
    [Range(1, int.MaxValue)] int Amount,
    [Range(0.01, double.MaxValue)] double Price);

public record DetailListRequest(int IdInvoice, [Required, MinLength(1)] List<DetailItemRequest> Details);