using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mc2.CrudTest.Query.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Mc2CodeChallenge");

            migrationBuilder.CreateTable(
                name: "Customer",
                schema: "Mc2CodeChallenge",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: false),
                    Email = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    BankAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhoneNumber",
                schema: "Mc2CodeChallenge",
                columns: table => new
                {
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Phone = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumber", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_PhoneNumber_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Mc2CodeChallenge",
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_DateOfBirth_FirstName_LastName",
                schema: "Mc2CodeChallenge",
                table: "Customer",
                columns: new[] { "DateOfBirth", "FirstName", "LastName" });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_Email",
                schema: "Mc2CodeChallenge",
                table: "Customer",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhoneNumber",
                schema: "Mc2CodeChallenge");

            migrationBuilder.DropTable(
                name: "Customer",
                schema: "Mc2CodeChallenge");
        }
    }
}
