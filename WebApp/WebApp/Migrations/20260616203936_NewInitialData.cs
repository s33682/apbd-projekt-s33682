using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    /// <inheritdoc />
    public partial class NewInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Contracts",
                columns: new[] { "ContractId", "AdditionalSupportYears", "ClientId", "DiscountId", "FullPrice", "IsActive", "IsClientLoyal", "IsPaid", "MaximumPaymentDate", "MinimumPaymentDate", "SoftVersionId" },
                values: new object[] { 3, 0, 2, null, 5000.00m, true, false, false, new DateOnly(2026, 6, 1), new DateOnly(2026, 5, 10), 1 });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "SubscriptionId", "BillingPeriodId", "ClientId", "DiscountId", "IsActive", "IsClientLoyal", "Name", "PeriodEndDate", "PeriodPrice", "PeriodStartDate", "SoftVersionId", "SubscriptionStartDate" },
                values: new object[] { 3, 1, 1, null, true, false, "Porzucona subskrypcja", new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 200.00m, new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "ContractPayments",
                columns: new[] { "PaymentId", "Amount", "ContractId", "CreatedAt", "IsRefunded" },
                values: new object[] { 4, 1000.00m, 3, new DateTime(2026, 5, 15, 10, 0, 0, 0, DateTimeKind.Unspecified), false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ContractPayments",
                keyColumn: "PaymentId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "SubscriptionId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Contracts",
                keyColumn: "ContractId",
                keyValue: 3);
        }
    }
}
