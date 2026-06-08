using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApp.Migrations
{
    /// <inheritdoc />
    public partial class InitCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Number = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Apartment = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                });

            migrationBuilder.CreateTable(
                name: "BillingPeriods",
                columns: table => new
                {
                    PeriodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MonthsNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingPeriods", x => x.PeriodId);
                });

            migrationBuilder.CreateTable(
                name: "DiscountTypes",
                columns: table => new
                {
                    DiscountTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Offer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountTypes", x => x.DiscountTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "SoftCategories",
                columns: table => new
                {
                    SoftCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoftCategories", x => x.SoftCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PhoneNumber = table.Column<decimal>(type: "decimal(9,0)", precision: 9, scale: 0, nullable: false),
                    KrsNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyId);
                    table.ForeignKey(
                        name: "FK_Companies_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Individuals",
                columns: table => new
                {
                    IndividualId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PhoneNumber = table.Column<decimal>(type: "decimal(9,0)", precision: 9, scale: 0, nullable: false),
                    Pesel = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Individuals", x => x.IndividualId);
                    table.ForeignKey(
                        name: "FK_Individuals_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    DiscountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DiscountTypeId = table.Column<int>(type: "int", nullable: false),
                    Percentage = table.Column<decimal>(type: "decimal(3,0)", precision: 3, scale: 0, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.DiscountId);
                    table.ForeignKey(
                        name: "FK_Discounts_DiscountTypes_DiscountTypeId",
                        column: x => x.DiscountTypeId,
                        principalTable: "DiscountTypes",
                        principalColumn: "DiscountTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employees_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Software",
                columns: table => new
                {
                    SoftwareId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoftCategoryId = table.Column<int>(type: "int", nullable: false),
                    LicensePricePerYear = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Software", x => x.SoftwareId);
                    table.ForeignKey(
                        name: "FK_Software_SoftCategories_SoftCategoryId",
                        column: x => x.SoftCategoryId,
                        principalTable: "SoftCategories",
                        principalColumn: "SoftCategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IndividualId = table.Column<int>(type: "int", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                    table.ForeignKey(
                        name: "FK_Clients_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "CompanyId");
                    table.ForeignKey(
                        name: "FK_Clients_Individuals_IndividualId",
                        column: x => x.IndividualId,
                        principalTable: "Individuals",
                        principalColumn: "IndividualId");
                });

            migrationBuilder.CreateTable(
                name: "SoftVersions",
                columns: table => new
                {
                    SoftVersionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VersionNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    IsNewest = table.Column<bool>(type: "bit", nullable: false),
                    SoftwareId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoftVersions", x => x.SoftVersionId);
                    table.ForeignKey(
                        name: "FK_SoftVersions_Software_SoftwareId",
                        column: x => x.SoftwareId,
                        principalTable: "Software",
                        principalColumn: "SoftwareId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    ContractId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    SoftVersionId = table.Column<int>(type: "int", nullable: false),
                    MinimumPaymentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    MaximumPaymentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DiscountId = table.Column<int>(type: "int", nullable: true),
                    IsClientLoyal = table.Column<bool>(type: "bit", nullable: false),
                    AdditionalSupportYears = table.Column<int>(type: "int", nullable: true),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    FullPrice = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.ContractId);
                    table.ForeignKey(
                        name: "FK_Contracts_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracts_Discounts_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discounts",
                        principalColumn: "DiscountId");
                    table.ForeignKey(
                        name: "FK_Contracts_SoftVersions_SoftVersionId",
                        column: x => x.SoftVersionId,
                        principalTable: "SoftVersions",
                        principalColumn: "SoftVersionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    SubscriptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    SoftVersionId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BillingPeriodId = table.Column<int>(type: "int", nullable: false),
                    PeriodPrice = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiscountId = table.Column<int>(type: "int", nullable: true),
                    IsClientLoyal = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.SubscriptionId);
                    table.ForeignKey(
                        name: "FK_Subscriptions_BillingPeriods_BillingPeriodId",
                        column: x => x.BillingPeriodId,
                        principalTable: "BillingPeriods",
                        principalColumn: "PeriodId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Discounts_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discounts",
                        principalColumn: "DiscountId");
                    table.ForeignKey(
                        name: "FK_Subscriptions_SoftVersions_SoftVersionId",
                        column: x => x.SoftVersionId,
                        principalTable: "SoftVersions",
                        principalColumn: "SoftVersionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractPayments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    IsRefunded = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractPayments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_ContractPayments_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "ContractId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPayments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscriptionId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPayments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_SubscriptionPayments_Subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscriptions",
                        principalColumn: "SubscriptionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "AddressId", "Apartment", "City", "Number", "Street", "ZipCode" },
                values: new object[,]
                {
                    { 1, "10", "Warszawa", "70b", "Łopuszańska", "02-230" },
                    { 2, null, "Kraków", "15", "Floriańska", "30-001" },
                    { 3, "2", "Gdańsk", "5", "Długa", "80-001" }
                });

            migrationBuilder.InsertData(
                table: "BillingPeriods",
                columns: new[] { "PeriodId", "MonthsNumber", "Type" },
                values: new object[,]
                {
                    { 1, 1, "Miesięczny" },
                    { 2, 12, "Roczny" }
                });

            migrationBuilder.InsertData(
                table: "DiscountTypes",
                columns: new[] { "DiscountTypeId", "Offer" },
                values: new object[,]
                {
                    { 1, "Black Friday" },
                    { 2, "Promocja Wiosenna" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Pracownik" }
                });

            migrationBuilder.InsertData(
                table: "SoftCategories",
                columns: new[] { "SoftCategoryId", "Name" },
                values: new object[,]
                {
                    { 1, "Finanse i Księgowość" },
                    { 2, "Edukacja" }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "CompanyId", "AddressId", "Email", "KrsNumber", "Name", "PhoneNumber" },
                values: new object[] { 1, 3, "kontakt@techcorp.pl", "0000123456", "TechCorp Sp. z o.o.", 555666777m });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "DiscountId", "DiscountTypeId", "EndDate", "Name", "Percentage", "StartDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Black Friday 2026", 20.0m, new DateTime(2026, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, new DateTime(2026, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wiosna 2026", 10.0m, new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "Login", "Password", "RoleId" },
                values: new object[,]
                {
                    { 1, "admin", "AQAAAAIAAYagAAAAECOSfrReTZmf3D5jx4FjggFLBBHDyiSjXoaa7e9vIynzts02MN1zw4/kahgjiiALYw==", 1 },
                    { 2, "j.kowalski", "AQAAAAIAAYagAAAAEFFuLGR1ug884ZyX58kcSf1aQ6HCt5fF/yNY1g2g9E6SqPkDmVCR/7ItPJGJFupO9g==", 2 }
                });

            migrationBuilder.InsertData(
                table: "Individuals",
                columns: new[] { "IndividualId", "AddressId", "Email", "FirstName", "IsActive", "LastName", "Pesel", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, 1, "jan.kowalski@wp.pl", "Jan", true, "Kowalski", "90010112345", 123456789m },
                    { 2, 2, "anna.nowak@gmail.com", "Anna", true, "Nowak", "95050554321", 987654321m }
                });

            migrationBuilder.InsertData(
                table: "Software",
                columns: new[] { "SoftwareId", "Description", "LicensePricePerYear", "Name", "SoftCategoryId" },
                values: new object[,]
                {
                    { 1, "System do pełnej księgowości.", 5000.00m, "Księgowość Pro", 1 },
                    { 2, "Zarządzanie uczelnią wyższą.", 12000.00m, "EduPlatform", 2 }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "ClientId", "CompanyId", "IndividualId" },
                values: new object[,]
                {
                    { 1, null, 1 },
                    { 2, null, 2 },
                    { 3, 1, null }
                });

            migrationBuilder.InsertData(
                table: "SoftVersions",
                columns: new[] { "SoftVersionId", "IsNewest", "SoftwareId", "VersionNumber" },
                values: new object[,]
                {
                    { 1, false, 1, "1.0" },
                    { 2, true, 1, "2.0" },
                    { 3, true, 2, "1.5" }
                });

            migrationBuilder.InsertData(
                table: "Contracts",
                columns: new[] { "ContractId", "AdditionalSupportYears", "ClientId", "DiscountId", "FullPrice", "IsActive", "IsClientLoyal", "IsPaid", "MaximumPaymentDate", "MinimumPaymentDate", "SoftVersionId" },
                values: new object[,]
                {
                    { 1, 0, 1, null, 5000.00m, true, false, true, new DateOnly(2026, 6, 14), new DateOnly(2026, 6, 1), 2 },
                    { 2, 2, 3, 2, 14000.00m, true, true, false, new DateOnly(2026, 6, 25), new DateOnly(2026, 6, 5), 3 }
                });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "SubscriptionId", "BillingPeriodId", "ClientId", "DiscountId", "EndDate", "IsActive", "IsClientLoyal", "Name", "PeriodPrice", "SoftVersionId", "StartDate" },
                values: new object[,]
                {
                    { 1, 1, 2, null, new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, "Subskrypcja Księgowość Standard", 500.00m, 1, new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, 3, 2, new DateTime(2027, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, "Subskrypcja EduPlatform Premium", 10000.00m, 3, new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "ContractPayments",
                columns: new[] { "PaymentId", "Amount", "ContractId", "CreatedAt", "IsRefunded" },
                values: new object[,]
                {
                    { 1, 5000.00m, 1, new DateTime(2026, 6, 5, 10, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 2, 4000.00m, 2, new DateTime(2026, 6, 10, 12, 30, 0, 0, DateTimeKind.Unspecified), false },
                    { 3, 4000.00m, 2, new DateTime(2026, 6, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), false }
                });

            migrationBuilder.InsertData(
                table: "SubscriptionPayments",
                columns: new[] { "PaymentId", "Amount", "PaymentDate", "SubscriptionId" },
                values: new object[,]
                {
                    { 1, 500.00m, new DateTime(2026, 6, 1, 9, 15, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, 10000.00m, new DateTime(2025, 5, 10, 11, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 3, 9500.00m, new DateTime(2026, 5, 9, 10, 20, 0, 0, DateTimeKind.Unspecified), 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_CompanyId",
                table: "Clients",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_IndividualId",
                table: "Clients",
                column: "IndividualId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_AddressId",
                table: "Companies",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_KrsNumber",
                table: "Companies",
                column: "KrsNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContractPayments_ContractId",
                table: "ContractPayments",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ClientId",
                table: "Contracts",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_DiscountId",
                table: "Contracts",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_SoftVersionId",
                table: "Contracts",
                column: "SoftVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_DiscountTypeId",
                table: "Discounts",
                column: "DiscountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_RoleId",
                table: "Employees",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Individuals_AddressId",
                table: "Individuals",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Individuals_Pesel",
                table: "Individuals",
                column: "Pesel",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SoftVersions_SoftwareId",
                table: "SoftVersions",
                column: "SoftwareId");

            migrationBuilder.CreateIndex(
                name: "IX_Software_SoftCategoryId",
                table: "Software",
                column: "SoftCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPayments_SubscriptionId",
                table: "SubscriptionPayments",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_BillingPeriodId",
                table: "Subscriptions",
                column: "BillingPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_ClientId",
                table: "Subscriptions",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_DiscountId",
                table: "Subscriptions",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_SoftVersionId",
                table: "Subscriptions",
                column: "SoftVersionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractPayments");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "SubscriptionPayments");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "BillingPeriods");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "SoftVersions");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Individuals");

            migrationBuilder.DropTable(
                name: "DiscountTypes");

            migrationBuilder.DropTable(
                name: "Software");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "SoftCategories");
        }
    }
}
