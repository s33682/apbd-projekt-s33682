using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    /// <inheritdoc />
    public partial class RevorkedSubscription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Subscriptions",
                newName: "SubscriptionStartDate");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Subscriptions",
                newName: "PeriodStartDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "PeriodEndDate",
                table: "Subscriptions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Individuals",
                type: "nvarchar(9)",
                maxLength: 9,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,0)",
                oldPrecision: 9);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Companies",
                type: "nvarchar(9)",
                maxLength: 9,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,0)",
                oldPrecision: 9);

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "CompanyId",
                keyValue: 1,
                column: "PhoneNumber",
                value: "555666777");

            migrationBuilder.UpdateData(
                table: "Individuals",
                keyColumn: "IndividualId",
                keyValue: 1,
                column: "PhoneNumber",
                value: "123456789");

            migrationBuilder.UpdateData(
                table: "Individuals",
                keyColumn: "IndividualId",
                keyValue: 2,
                column: "PhoneNumber",
                value: "987654321");

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "SubscriptionId",
                keyValue: 1,
                columns: new[] { "PeriodEndDate", "PeriodStartDate" },
                values: new object[] { new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "SubscriptionId",
                keyValue: 2,
                columns: new[] { "PeriodEndDate", "PeriodStartDate" },
                values: new object[] { new DateTime(2027, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PeriodEndDate",
                table: "Subscriptions");

            migrationBuilder.RenameColumn(
                name: "SubscriptionStartDate",
                table: "Subscriptions",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "PeriodStartDate",
                table: "Subscriptions",
                newName: "EndDate");

            migrationBuilder.AlterColumn<decimal>(
                name: "PhoneNumber",
                table: "Individuals",
                type: "decimal(9,0)",
                precision: 9,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(9)",
                oldMaxLength: 9);

            migrationBuilder.AlterColumn<decimal>(
                name: "PhoneNumber",
                table: "Companies",
                type: "decimal(9,0)",
                precision: 9,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(9)",
                oldMaxLength: 9);

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "CompanyId",
                keyValue: 1,
                column: "PhoneNumber",
                value: 555666777m);

            migrationBuilder.UpdateData(
                table: "Individuals",
                keyColumn: "IndividualId",
                keyValue: 1,
                column: "PhoneNumber",
                value: 123456789m);

            migrationBuilder.UpdateData(
                table: "Individuals",
                keyColumn: "IndividualId",
                keyValue: 2,
                column: "PhoneNumber",
                value: 987654321m);

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "SubscriptionId",
                keyValue: 1,
                column: "EndDate",
                value: new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Subscriptions",
                keyColumn: "SubscriptionId",
                keyValue: 2,
                column: "EndDate",
                value: new DateTime(2027, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
