using WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Context;

public class AppDbContext : DbContext
{
    public DbSet<Address> Addresses { get; set; }
    public DbSet<BillingPeriod> BillingPeriods { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<ContractPayment> ContractPayments { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<DiscountType> DiscountTypes { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Individual> Individuals { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<SoftCategory> SoftCategories { get; set; }
    public DbSet<SoftVersion> SoftVersions { get; set; }
    public DbSet<Software> Software { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<SubscriptionPayment> SubscriptionPayments { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Role>().HasData(
            new Role { RoleId = 1, RoleName = "Admin" },
            new Role { RoleId = 2, RoleName = "Pracownik" }
        );

        modelBuilder.Entity<Employee>().HasData(
            new Employee { EmployeeId = 1, Login = "admin", Password = "AQAAAAIAAYagAAAAECOSfrReTZmf3D5jx4FjggFLBBHDyiSjXoaa7e9vIynzts02MN1zw4/kahgjiiALYw==", RoleId = 1 },
            new Employee { EmployeeId = 2, Login = "j.kowalski", Password = "AQAAAAIAAYagAAAAEFFuLGR1ug884ZyX58kcSf1aQ6HCt5fF/yNY1g2g9E6SqPkDmVCR/7ItPJGJFupO9g==", RoleId = 2 }
        );

        modelBuilder.Entity<Address>().HasData(
            new Address { AddressId = 1, City = "Warszawa", ZipCode = "02-230", Street = "Łopuszańska", Number = "70b", Apartment = "10" },
            new Address { AddressId = 2, City = "Kraków", ZipCode = "30-001", Street = "Floriańska", Number = "15", Apartment = null },
            new Address { AddressId = 3, City = "Gdańsk", ZipCode = "80-001", Street = "Długa", Number = "5", Apartment = "2" }
        );

        modelBuilder.Entity<Individual>().HasData(
            new Individual { IndividualId = 1, FirstName = "Jan", LastName = "Kowalski", AddressId = 1, Email = "jan.kowalski@wp.pl", PhoneNumber = "123456789", Pesel = "90010112345", IsActive = true },
            new Individual { IndividualId = 2, FirstName = "Anna", LastName = "Nowak", AddressId = 2, Email = "anna.nowak@gmail.com", PhoneNumber = "987654321", Pesel = "95050554321", IsActive = true }
        );

        modelBuilder.Entity<Company>().HasData(
            new Company { CompanyId = 1, Name = "TechCorp Sp. z o.o.", AddressId = 3, Email = "kontakt@techcorp.pl", PhoneNumber = "555666777", KrsNumber = "0000123456" }
        );

        modelBuilder.Entity<Client>().HasData(
            new Client { ClientId = 1, IndividualId = 1, CompanyId = null },
            new Client { ClientId = 2, IndividualId = 2, CompanyId = null },
            new Client { ClientId = 3, IndividualId = null, CompanyId = 1 }
        );

        modelBuilder.Entity<SoftCategory>().HasData(
            new SoftCategory { SoftCategoryId = 1, Name = "Finanse i Księgowość" },
            new SoftCategory { SoftCategoryId = 2, Name = "Edukacja" }
        );

        modelBuilder.Entity<Software>().HasData(
            new Software { SoftwareId = 1, Name = "Księgowość Pro", Description = "System do pełnej księgowości.", SoftCategoryId = 1, LicensePricePerYear = 5000.00m },
            new Software { SoftwareId = 2, Name = "EduPlatform", Description = "Zarządzanie uczelnią wyższą.", SoftCategoryId = 2, LicensePricePerYear = 12000.00m }
        );

        modelBuilder.Entity<SoftVersion>().HasData(
            new SoftVersion { SoftVersionId = 1, VersionNumber = "1.0", IsNewest = false, SoftwareId = 1 },
            new SoftVersion { SoftVersionId = 2, VersionNumber = "2.0", IsNewest = true, SoftwareId = 1 },
            new SoftVersion { SoftVersionId = 3, VersionNumber = "1.5", IsNewest = true, SoftwareId = 2 }
        );

        modelBuilder.Entity<DiscountType>().HasData(
            new DiscountType { DiscountTypeId = 1, Offer = "Black Friday" },
            new DiscountType { DiscountTypeId = 2, Offer = "Promocja Wiosenna" }
        );

        modelBuilder.Entity<Discount>().HasData(
            new Discount { DiscountId = 1, Name = "Black Friday 2026", DiscountTypeId = 1, Percentage = 20.0m, StartDate = new DateTime(2026, 11, 20), EndDate = new DateTime(2026, 11, 30) },
            new Discount { DiscountId = 2, Name = "Wiosna 2026", DiscountTypeId = 2, Percentage = 10.0m, StartDate = new DateTime(2026, 3, 1), EndDate = new DateTime(2026, 5, 31) }
        );

        modelBuilder.Entity<BillingPeriod>().HasData(
            new BillingPeriod { PeriodId = 1, Type = "Miesięczny", MonthsNumber = 1 },
            new BillingPeriod { PeriodId = 2, Type = "Roczny", MonthsNumber = 12 }
        );
        
        // Kontrakt opłacony w całości, Klient 1
        modelBuilder.Entity<Contract>().HasData(
            new Contract { 
                ContractId = 1, ClientId = 1, SoftVersionId = 2, 
                MinimumPaymentDate = new DateOnly(2026, 6, 1), MaximumPaymentDate = new DateOnly(2026, 6, 14), 
                IsActive = true, DiscountId = null, IsClientLoyal = false, AdditionalSupportYears = 0, 
                IsPaid = true, FullPrice = 5000.00m 
            }
        );

        modelBuilder.Entity<ContractPayment>().HasData(
            new ContractPayment { PaymentId = 1, ContractId = 1, Amount = 5000.00m, IsRefunded = false, CreatedAt = new DateTime(2026, 6, 5, 10, 0, 0) }
        );

        // Kontrakt opłacany w ratach, JESZCZE NIEOPŁACONY w pełni, (Klient 3 Firma), (Do zapłaty 12000, zapłacono 8000)
        modelBuilder.Entity<Contract>().HasData(
            new Contract { 
                ContractId = 2, ClientId = 3, SoftVersionId = 3, 
                MinimumPaymentDate = new DateOnly(2026, 6, 5), MaximumPaymentDate = new DateOnly(2026, 6, 25), 
                IsActive = true, DiscountId = 2, IsClientLoyal = true, AdditionalSupportYears = 2, 
                IsPaid = false, FullPrice = 14000.00m
            }
        );

        modelBuilder.Entity<ContractPayment>().HasData(
            new ContractPayment { PaymentId = 2, ContractId = 2, Amount = 4000.00m, IsRefunded = false, CreatedAt = new DateTime(2026, 6, 10, 12, 30, 0) },
            new ContractPayment { PaymentId = 3, ContractId = 2, Amount = 4000.00m, IsRefunded = false, CreatedAt = new DateTime(2026, 6, 15, 14, 00, 0) }
        );
        
        // Nowa subskrypcja (miesięczna), opłacony 1 okres, Klient 2
        modelBuilder.Entity<Subscription>().HasData(
            new Subscription {
                SubscriptionId = 1, ClientId = 2, SoftVersionId = 1, Name = "Subskrypcja Księgowość Standard",
                BillingPeriodId = 1, PeriodPrice = 500.00m, 
                SubscriptionStartDate = new DateTime(2026, 6, 1, 0, 0, 0),
                PeriodStartDate = new DateTime(2026, 6, 1, 0, 0, 0), 
                PeriodEndDate = new DateTime(2026, 7, 1, 0, 0, 0),
                DiscountId = null, IsClientLoyal = false, IsActive = true
            }
        );

        modelBuilder.Entity<SubscriptionPayment>().HasData(
            new SubscriptionPayment { PaymentId = 1, SubscriptionId = 1, Amount = 500.00m, PaymentDate = new DateTime(2026, 6, 1, 9, 15, 0) }
        );

        // Trwająca subskrypcja (roczna), przedłużona na kolejny rok, Klient 3 (Firma)
        modelBuilder.Entity<Subscription>().HasData(
            new Subscription {
                SubscriptionId = 2, ClientId = 3, SoftVersionId = 3, Name = "Subskrypcja EduPlatform Premium",
                BillingPeriodId = 2, PeriodPrice = 10000.00m, 
                SubscriptionStartDate = new DateTime(2025, 5, 10, 0, 0, 0),
                PeriodStartDate = new DateTime(2026, 5, 10, 0, 0, 0),
                PeriodEndDate = new DateTime(2027, 5, 10, 0, 0, 0),
                DiscountId = 2, IsClientLoyal = true, IsActive = true
            }
        );

        modelBuilder.Entity<SubscriptionPayment>().HasData(
            new SubscriptionPayment { PaymentId = 2, SubscriptionId = 2, Amount = 10000.00m, PaymentDate = new DateTime(2025, 5, 10, 11, 0, 0) },
            new SubscriptionPayment { PaymentId = 3, SubscriptionId = 2, Amount = 9500.00m, PaymentDate = new DateTime(2026, 5, 9, 10, 20, 0) }
        );
        
        // Subskrypcja i kontrakt dla hangfire
        // Przeterminowany kontrakt
        modelBuilder.Entity<Contract>().HasData(
            new Contract { 
                ContractId = 3, ClientId = 2, SoftVersionId = 1, 
                MinimumPaymentDate = new DateOnly(2026, 5, 10), 
                MaximumPaymentDate = new DateOnly(2026, 6, 1),
                IsActive = true, DiscountId = null, IsClientLoyal = false, AdditionalSupportYears = 0, 
                IsPaid = false, FullPrice = 5000.00m 
            }
        );
        modelBuilder.Entity<ContractPayment>().HasData(
            new ContractPayment { PaymentId = 4, ContractId = 3, Amount = 1000.00m, IsRefunded = false, CreatedAt = new DateTime(2026, 5, 15, 10, 0, 0) }
        );
        // Przeterminowana subskrypcja
        modelBuilder.Entity<Subscription>().HasData(
            new Subscription {
                SubscriptionId = 3, ClientId = 1, SoftVersionId = 2, Name = "Porzucona subskrypcja",
                BillingPeriodId = 1, PeriodPrice = 200.00m, 
                SubscriptionStartDate = new DateTime(2026, 1, 1, 0, 0, 0),
                PeriodStartDate = new DateTime(2026, 4, 1, 0, 0, 0), 
                PeriodEndDate = new DateTime(2026, 5, 1, 0, 0, 0),
                DiscountId = null, IsClientLoyal = false, IsActive = true
            }
        );
    }
}