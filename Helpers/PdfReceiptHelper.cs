using EInsurance.Models.DTOs;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace EInsurance.Helpers
{
    public class PdfReceiptHelper
    {
        public byte[] GenerateReceipt(PaymentReceiptDto receipt)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    //  Header
                    page.Header().Column(col =>
                    {
                        // Top row — company + receipt title
                        col.Item().Row(row =>
                        {
                            // Left — company name
                            row.RelativeItem().Column(c =>
                            {
                                c.Item()
                                    .Text("eInsurance")
                                    .FontSize(22)
                                    .Bold()
                                    .FontColor("#185FA5");

                                c.Item()
                                    .Text("Online Insurance Management System")
                                    .FontSize(10)
                                    .FontColor(Colors.Grey.Darken1);
                            });

                            // Right — receipt info
                            row.RelativeItem().AlignRight().Column(c =>
                            {
                                c.Item()
                                    .AlignRight()
                                    .Text("PAYMENT RECEIPT")
                                    .FontSize(18)
                                    .Bold()
                                    .FontColor("#185FA5");

                                c.Item()
                                    .AlignRight()
                                    .Text(receipt.ReceiptNumber)
                                    .FontSize(11)
                                    .Bold()
                                    .FontColor("#0F6E56");
                            });
                        });

                        // Divider line — inside same Column
                        col.Item()
                            .PaddingTop(8)
                            .LineHorizontal(1)
                            .LineColor(Colors.Grey.Lighten2);
                    });

                    // Content
                    page.Content()
                        .PaddingTop(15)
                        .Column(col =>
                        {
                            // Success Banner
                            col.Item()
                                .Background("#0F6E56")
                                .Padding(10)
                                .AlignCenter()
                                .Text("PAYMENT SUCCESSFUL")
                                .FontSize(14)
                                .Bold()
                                .FontColor(Colors.White);

                            col.Item().Height(15);

                            // Two Column Info Row
                            col.Item().Row(row =>
                            {
                                // Customer Details
                                row.RelativeItem()
                                    .Border(1)
                                    .BorderColor(Colors.Grey.Lighten2)
                                    .Background("#F1EFE8")
                                    .Padding(12)
                                    .Column(c =>
                                    {
                                        c.Item()
                                            .Text("CUSTOMER DETAILS")
                                            .FontSize(10)
                                            .Bold()
                                            .FontColor("#185FA5");

                                        c.Item().Height(8);

                                        AddInfoRow(c, "Name",
                                            receipt.CustomerName);
                                        AddInfoRow(c, "Email",
                                            receipt.CustomerEmail);
                                        AddInfoRow(c, "Phone",
                                            receipt.CustomerPhone);
                                        AddInfoRow(c, "Agent",
                                            receipt.AgentName);
                                    });

                                row.ConstantItem(10);

                                // Payment Details
                                row.RelativeItem()
                                    .Border(1)
                                    .BorderColor(Colors.Grey.Lighten2)
                                    .Background("#F1EFE8")
                                    .Padding(12)
                                    .Column(c =>
                                    {
                                        c.Item()
                                            .Text("PAYMENT DETAILS")
                                            .FontSize(10)
                                            .Bold()
                                            .FontColor("#185FA5");

                                        c.Item().Height(8);

                                        AddInfoRow(c, "Receipt No",
                                            receipt.ReceiptNumber);
                                        AddInfoRow(c, "Date",
                                            receipt.PaymentDate
                                                .ToString("dd MMM yyyy"));
                                        AddInfoRow(c, "Policy ID",
                                            "#" + receipt.PolicyID);
                                        AddInfoRow(c, "Amount",
                                            "Rs." +
                                            receipt.Amount.ToString("N2"));
                                    });
                            });

                            col.Item().Height(15);

                            // Policy Info Header
                            col.Item()
                                .Text("POLICY INFORMATION")
                                .FontSize(12)
                                .Bold()
                                .FontColor("#185FA5");

                            col.Item().Height(5);

                            // Policy Table
                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(cols =>
                                {
                                    cols.RelativeColumn(2);
                                    cols.RelativeColumn(3);
                                });

                                AddTableRow(table, "Plan",
                                    receipt.PlanName, true);
                                AddTableRow(table, "Scheme",
                                    receipt.SchemeName, false);
                                AddTableRow(table, "Policy Details",
                                    receipt.PolicyDetails, true);
                                AddTableRow(table, "Monthly Premium",
                                    "Rs." + receipt.Premium
                                        .ToString("N2"), false);
                                AddTableRow(table, "Date Issued",
                                    receipt.DateIssued
                                        .ToString("dd MMM yyyy"), true);
                                AddTableRow(table, "Lapse Date",
                                    receipt.PolicyLapseDate
                                        .ToString("dd MMM yyyy"), false);
                            });

                            col.Item().Height(15);

                            // Amount Box
                            col.Item()
                                .Background("#185FA5")
                                .Padding(20)
                                .Column(c =>
                                {
                                    c.Item()
                                        .AlignCenter()
                                        .Text("AMOUNT PAID")
                                        .FontSize(12)
                                        .FontColor(Colors.White);

                                    c.Item()
                                        .AlignCenter()
                                        .Text("Rs." +
                                            receipt.Amount.ToString("N2"))
                                        .FontSize(28)
                                        .Bold()
                                        .FontColor(Colors.White);
                                });
                        });

                    // Footer
                    page.Footer().Column(col =>
                    {
                        col.Item()
                            .LineHorizontal(1)
                            .LineColor(Colors.Grey.Lighten2);

                        col.Item()
                            .PaddingTop(5)
                            .AlignCenter()
                            .Text(text =>
                            {
                                text.Line(
                                    "This is a computer generated " +
                                    "receipt and does not require " +
                                    "a signature.")
                                    .FontSize(8)
                                    .FontColor(Colors.Grey.Darken1);

                                text.Line(
                                    "For queries: " +
                                    "support@einsurance.com  |  " +
                                    "Generated: " +
                                    DateTime.Now
                                        .ToString("dd MMM yyyy HH:mm"))
                                    .FontSize(8)
                                    .FontColor(Colors.Grey.Darken1);
                            });
                    });
                });
            });

            return document.GeneratePdf();
        }

        // Helper — Info Row
        private void AddInfoRow(
            ColumnDescriptor col,
            string label, string value)
        {
            col.Item().Row(row =>
            {
                row.ConstantItem(85)
                    .Text(label + ":")
                    .Bold()
                    .FontSize(9);

                row.RelativeItem()
                    .Text(value)
                    .FontSize(9);
            });

            col.Item().Height(4);
        }

        //  Helper — Table Row 
        private void AddTableRow(
            TableDescriptor table,
            string label, string value, bool shaded)
        {
            var bg = shaded ? "#F1EFE8" : "#FFFFFF";

            table.Cell()
                .Background(bg)
                .Padding(8)
                .Text(label)
                .Bold()
                .FontSize(10);

            table.Cell()
                .Background(bg)
                .Padding(8)
                .Text(value)
                .FontSize(10);
        }
    }
}