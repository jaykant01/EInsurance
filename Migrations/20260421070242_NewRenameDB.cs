using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EInsurance.Migrations
{
    /// <inheritdoc />
    public partial class NewRenameDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commissions_Policy_PolicyID",
                table: "Commissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_InsuranceAgents_AgentID",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Customer_CustomerID",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Policy_PolicyID",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Policy_Customer_CustomerID",
                table: "Policy");

            migrationBuilder.DropForeignKey(
                name: "FK_Policy_Schemes_SchemeID",
                table: "Policy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Policy",
                table: "Policy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                table: "Customer");

            migrationBuilder.RenameTable(
                name: "Policy",
                newName: "Policies");

            migrationBuilder.RenameTable(
                name: "Customer",
                newName: "Customers");

            migrationBuilder.RenameColumn(
                name: "Preminum",
                table: "Policies",
                newName: "Premium");

            migrationBuilder.RenameIndex(
                name: "IX_Policy_SchemeID",
                table: "Policies",
                newName: "IX_Policies_SchemeID");

            migrationBuilder.RenameIndex(
                name: "IX_Policy_CustomerID",
                table: "Policies",
                newName: "IX_Policies_CustomerID");

            migrationBuilder.RenameIndex(
                name: "IX_Customer_Email",
                table: "Customers",
                newName: "IX_Customers_Email");

            migrationBuilder.RenameIndex(
                name: "IX_Customer_AgentID",
                table: "Customers",
                newName: "IX_Customers_AgentID");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Payment",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PolicyLapseDate",
                table: "Policies",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateIssued",
                table: "Policies",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Policies",
                table: "Policies",
                column: "PolicyID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "CustomerID");

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2026, 4, 21, 7, 2, 39, 63, DateTimeKind.Utc).AddTicks(3310), "$2a$11$UWVaTSg6ME.c2Cp0D/eoceeDhEkgGq33ugAtyTzBVLDDO/7gyLk.u" });

            migrationBuilder.AddForeignKey(
                name: "FK_Commissions_Policies_PolicyID",
                table: "Commissions",
                column: "PolicyID",
                principalTable: "Policies",
                principalColumn: "PolicyID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_InsuranceAgents_AgentID",
                table: "Customers",
                column: "AgentID",
                principalTable: "InsuranceAgents",
                principalColumn: "AgentID",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_Customers_CustomerID",
                table: "Policies",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_Schemes_SchemeID",
                table: "Policies",
                column: "SchemeID",
                principalTable: "Schemes",
                principalColumn: "SchemeID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commissions_Policies_PolicyID",
                table: "Commissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_InsuranceAgents_AgentID",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Customers_CustomerID",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Policies_PolicyID",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Policies_Customers_CustomerID",
                table: "Policies");

            migrationBuilder.DropForeignKey(
                name: "FK_Policies_Schemes_SchemeID",
                table: "Policies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Policies",
                table: "Policies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.RenameTable(
                name: "Policies",
                newName: "Policy");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Customer");

            migrationBuilder.RenameColumn(
                name: "Premium",
                table: "Policy",
                newName: "Preminum");

            migrationBuilder.RenameIndex(
                name: "IX_Policies_SchemeID",
                table: "Policy",
                newName: "IX_Policy_SchemeID");

            migrationBuilder.RenameIndex(
                name: "IX_Policies_CustomerID",
                table: "Policy",
                newName: "IX_Policy_CustomerID");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_Email",
                table: "Customer",
                newName: "IX_Customer_Email");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_AgentID",
                table: "Customer",
                newName: "IX_Customer_AgentID");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Payment",
                type: "decimal(10,2",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "PolicyLapseDate",
                table: "Policy",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DateIssued",
                table: "Policy",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Policy",
                table: "Policy",
                column: "PolicyID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                table: "Customer",
                column: "CustomerID");

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "AdminID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password" },
                values: new object[] { new DateTime(2026, 4, 19, 17, 8, 58, 350, DateTimeKind.Utc).AddTicks(3928), "$2a$11$WK255GidHx9WR23v0Kc/EuzargiXXaJqHrmjCtKOn6ABrU4TJf5S." });

            migrationBuilder.AddForeignKey(
                name: "FK_Commissions_Policy_PolicyID",
                table: "Commissions",
                column: "PolicyID",
                principalTable: "Policy",
                principalColumn: "PolicyID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_InsuranceAgents_AgentID",
                table: "Customer",
                column: "AgentID",
                principalTable: "InsuranceAgents",
                principalColumn: "AgentID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Customer_CustomerID",
                table: "Payment",
                column: "CustomerID",
                principalTable: "Customer",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Policy_PolicyID",
                table: "Payment",
                column: "PolicyID",
                principalTable: "Policy",
                principalColumn: "PolicyID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Policy_Customer_CustomerID",
                table: "Policy",
                column: "CustomerID",
                principalTable: "Customer",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Policy_Schemes_SchemeID",
                table: "Policy",
                column: "SchemeID",
                principalTable: "Schemes",
                principalColumn: "SchemeID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
