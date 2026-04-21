using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EInsurance.Migrations
{
    /// <inheritdoc />
    public partial class NewRenameDB1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Customers_CustomerID",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Policies_PolicyID",
                table: "Payment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payment",
                table: "Payment");

            migrationBuilder.RenameTable(
                name: "Payment",
                newName: "Payments");

            migrationBuilder.RenameIndex(
                name: "IX_Payment_PolicyID",
                table: "Payments",
                newName: "IX_Payments_PolicyID");

            migrationBuilder.RenameIndex(
                name: "IX_Payment_CustomerID",
                table: "Payments",
                newName: "IX_Payments_CustomerID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payments",
                table: "Payments",
                column: "PaymentID");

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2026, 4, 21, 10, 53, 50, 197, DateTimeKind.Utc).AddTicks(4474), "$2a$11$Z4z0xBNNI1gcYwlFgg3PU.DOKN.OpCPtAnCbUzP8ZpYe69lf8zzIa" });

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Customers_CustomerID",
                table: "Payments",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Policies_PolicyID",
                table: "Payments",
                column: "PolicyID",
                principalTable: "Policies",
                principalColumn: "PolicyID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Customers_CustomerID",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Policies_PolicyID",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payments",
                table: "Payments");

            migrationBuilder.RenameTable(
                name: "Payments",
                newName: "Payment");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_PolicyID",
                table: "Payment",
                newName: "IX_Payment_PolicyID");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_CustomerID",
                table: "Payment",
                newName: "IX_Payment_CustomerID");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DateOfBirth",
                table: "Customers",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payment",
                table: "Payment",
                column: "PaymentID");

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2026, 4, 21, 7, 2, 39, 63, DateTimeKind.Utc).AddTicks(3310), "$2a$11$UWVaTSg6ME.c2Cp0D/eoceeDhEkgGq33ugAtyTzBVLDDO/7gyLk.u" });

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Customers_CustomerID",
                table: "Payment",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Policies_PolicyID",
                table: "Payment",
                column: "PolicyID",
                principalTable: "Policies",
                principalColumn: "PolicyID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
