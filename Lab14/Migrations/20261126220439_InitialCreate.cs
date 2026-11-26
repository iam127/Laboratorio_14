using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab14.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    idCustomers = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    DocumentNumber = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.idCustomers);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    idProducts = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.idProducts);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    idInvoices = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Customers_idCustomers = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.idInvoices);
                    table.ForeignKey(
                        name: "FK_Invoices_Customers_Customers_idCustomers",
                        column: x => x.Customers_idCustomers,
                        principalTable: "Customers",
                        principalColumn: "idCustomers",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Details",
                columns: table => new
                {
                    idDetails = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Products_idProducts = table.Column<int>(type: "int", nullable: false),
                    Invoices_idInvoices = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    SubTotal = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Details", x => x.idDetails);
                    table.ForeignKey(
                        name: "FK_Details_Invoices_Invoices_idInvoices",
                        column: x => x.Invoices_idInvoices,
                        principalTable: "Invoices",
                        principalColumn: "idInvoices",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Details_Products_Products_idProducts",
                        column: x => x.Products_idProducts,
                        principalTable: "Products",
                        principalColumn: "idProducts",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Details_Invoices_idInvoices",
                table: "Details",
                column: "Invoices_idInvoices");

            migrationBuilder.CreateIndex(
                name: "IX_Details_Products_idProducts",
                table: "Details",
                column: "Products_idProducts");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_Customers_idCustomers",
                table: "Invoices",
                column: "Customers_idCustomers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Details");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
