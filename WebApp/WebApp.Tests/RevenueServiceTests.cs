using Microsoft.EntityFrameworkCore;
using Moq;
using WebApp.Context;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Tests;

public class RevenueServiceTests
{
    private AppDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task GetActualRevenue_ShouldSumOnlyPaidContractsAndSubscriptions()
    {
        var dbContext = GetInMemoryDbContext();
        
        var mockCurrencyService = new Mock<ICurrencyService>();
        
        var softVersion = new SoftVersion { SoftVersionId = 1, VersionNumber = "1.0", IsNewest = true };
        dbContext.SoftVersions.Add(softVersion);
        
        var paidContract = new Contract { ContractId = 1, SoftVersionId = 1, IsPaid = true, FullPrice = 5000 };
        dbContext.Contracts.Add(paidContract);
        dbContext.ContractPayments.Add(new ContractPayment { ContractId = 1, Amount = 5000, IsRefunded = false });
        
        var unpaidContract = new Contract { ContractId = 2, SoftVersionId = 1, IsPaid = false, FullPrice = 3000 };
        dbContext.Contracts.Add(unpaidContract);
        dbContext.ContractPayments.Add(new ContractPayment { ContractId = 2, Amount = 1000, IsRefunded = false });
        
        var subscription = new Subscription { SubscriptionId = 1, SoftVersionId = 1, IsActive = true };
        dbContext.Subscriptions.Add(subscription);
        dbContext.SubscriptionPayments.Add(new SubscriptionPayment { SubscriptionId = 1, Amount = 500 });

        await dbContext.SaveChangesAsync();
        
        var revenueService = new RevenueService(dbContext, mockCurrencyService.Object);
        
        var result = await revenueService.GetActualRevenue(null, null);
        
        Assert.Equal(5500m, result.Amount);
        Assert.Equal("PLN", result.Currency);
    }

    [Fact]
    public async Task GetActualRevenue_WithCurrency_ShouldApplyExchangeRateCorrectly()
    {
        var dbContext = GetInMemoryDbContext();
        var mockCurrencyService = new Mock<ICurrencyService>();
        
        mockCurrencyService.Setup(s => s.GetRateAsync("USD")).ReturnsAsync(4.0m);

        var softVersion = new SoftVersion { SoftVersionId = 1, VersionNumber = "1.0", IsNewest = true };
        dbContext.SoftVersions.Add(softVersion);
        
        var paidContract = new Contract { ContractId = 1, SoftVersionId = 1, IsPaid = true, FullPrice = 8000 };
        dbContext.Contracts.Add(paidContract);
        dbContext.ContractPayments.Add(new ContractPayment { ContractId = 1, Amount = 8000, IsRefunded = false });

        await dbContext.SaveChangesAsync();

        var revenueService = new RevenueService(dbContext, mockCurrencyService.Object);
        
        var result = await revenueService.GetActualRevenue(null, "USD");
        
        Assert.Equal(2000m, result.Amount);
        Assert.Equal("USD", result.Currency);
    }
}