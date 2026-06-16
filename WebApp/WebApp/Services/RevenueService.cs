using Microsoft.EntityFrameworkCore;
using WebApp.Context;
using WebApp.DTOs;
using WebApp.Exceptions;

namespace WebApp.Services;

public class RevenueService : IRevenueService
{
    private readonly AppDbContext _context;
    private readonly ICurrencyService _currencyService;

    public RevenueService(AppDbContext context , ICurrencyService currencyService)
    {
        _context = context;
        _currencyService = currencyService;
    }

    public async Task<GetRevenueDetailsDto> GetActualRevenue(int? softwareId, string? currency)
    {
        if (softwareId != null)
        {
            var checkSoftware = await _context.Software.FirstOrDefaultAsync(s => s.SoftwareId == softwareId);
            if (checkSoftware == null)
            {
                throw new NotFoundException("Software with provided ID not found!");
            }
        }
        
        decimal currencyRate = 1;

        if (currency != null)
        {
            currency = currency.ToUpper();
            if (currency.Length != 3)
            {
                throw new NotPossibleException("Currency code must be in 3 letter ISO 4217 format!");
            }
            currencyRate = await _currencyService.GetRateAsync(currency);
        }

        var revenueContractQuery = _context.Contracts.AsQueryable();
        var revenueSubscriptionQuery = _context.Subscriptions.AsQueryable();
        if (softwareId != null)
        {
            revenueContractQuery = revenueContractQuery.Where(c => c.SoftVersion.SoftwareId == softwareId);
            revenueSubscriptionQuery = revenueSubscriptionQuery.Where( s=> s.SoftVersion.SoftwareId == softwareId);
        }
        var revenueContract = await revenueContractQuery.Where(c => c.IsPaid)
            .SumAsync(c => c.Payments.Where(p => !p.IsRefunded).Sum(p=>p.Amount));
        var revenueSubscription = await revenueSubscriptionQuery.SumAsync( s=> s.SubscriptionPayments.Sum(p=>p.Amount));

        var sumRevenue = Math.Round((revenueSubscription + revenueContract) / currencyRate, 2);

        var getRevenueDetails = new GetRevenueDetailsDto
        {
            Amount = sumRevenue,
            Currency =  currency ?? "PLN",
            SoftwareId =  softwareId
        };
        
        return getRevenueDetails;
    }

    public async Task<GetRevenueDetailsDto> GetPredictedRevenue(int? softwareId, string? currency)
    {
        if (softwareId != null)
        {
            var checkSoftware = await _context.Software.FirstOrDefaultAsync(s => s.SoftwareId == softwareId);
            if (checkSoftware == null)
            {
                throw new NotFoundException("Software with provided ID not found!");
            }
        }
        
        decimal currencyRate = 1;

        if (currency != null)
        {
            currency = currency.ToUpper();
            if (currency.Length != 3)
            {
                throw new NotPossibleException("Currency code must be in 3 letter ISO 4217 format!");
            }
            currencyRate = await _currencyService.GetRateAsync(currency);
        }

        var revenueContractQuery = _context.Contracts.AsQueryable();
        var revenueSubscriptionQuery = _context.Subscriptions.AsQueryable();
        if (softwareId != null)
        {
            revenueContractQuery = revenueContractQuery.Where(c => c.SoftVersion.SoftwareId == softwareId);
            revenueSubscriptionQuery = revenueSubscriptionQuery.Where( s=> s.SoftVersion.SoftwareId == softwareId);
        }
        var revenueContract = await revenueContractQuery.Where(c => c.IsActive || c.IsPaid)
            .SumAsync(c => c.FullPrice);
        var revenueSubscription = await revenueSubscriptionQuery.SumAsync( s=> s.SubscriptionPayments.Sum(p=>p.Amount));
        var predictedSubscription = await revenueSubscriptionQuery.Where(c => c.IsActive).SumAsync(s => s.PeriodPrice);

        var sumRevenue = Math.Round((revenueSubscription + revenueContract + predictedSubscription) / currencyRate, 2);

        var getRevenueDetails = new GetRevenueDetailsDto
        {
            Amount = sumRevenue,
            Currency =  currency ?? "PLN",
            SoftwareId =  softwareId
        };
        
        return getRevenueDetails;
    }
}