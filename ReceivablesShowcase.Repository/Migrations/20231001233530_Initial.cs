using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReceivablesShowcase.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Receivables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Reference = table.Column<string>(type: "TEXT", nullable: false),
                    PaidAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    PaidAmountCurrencyCode = table.Column<string>(type: "TEXT", maxLength: 8, nullable: true),
                    OpeningAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    OpeningAmountCurrencyCode = table.Column<string>(type: "TEXT", maxLength: 8, nullable: true),
                    IssueDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DueDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ClosedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Cancelled = table.Column<bool>(type: "INTEGER", nullable: false),
                    DebtorName = table.Column<string>(type: "TEXT", nullable: false),
                    DebtorReference = table.Column<string>(type: "TEXT", nullable: false),
                    DebtorRegistrationNumber = table.Column<string>(type: "TEXT", nullable: true),
                    DebtorAddressLine1 = table.Column<string>(type: "TEXT", nullable: true),
                    DebtorAddressLine2 = table.Column<string>(type: "TEXT", nullable: true),
                    DebtorAddressTown = table.Column<string>(type: "TEXT", nullable: true),
                    DebtorAddressState = table.Column<string>(type: "TEXT", nullable: true),
                    DebtorAddressZip = table.Column<string>(type: "TEXT", maxLength: 32, nullable: true),
                    DebtorAddressCountryCode = table.Column<string>(type: "TEXT", maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receivables", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Receivables_Reference",
                table: "Receivables",
                column: "Reference",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Receivables");
        }
    }
}
