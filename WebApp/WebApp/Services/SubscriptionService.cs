using Microsoft.EntityFrameworkCore;
using WebApp.Context;
using WebApp.DTOs;
using WebApp.Exceptions;
using WebApp.Models;

namespace WebApp.Services;

public class SubscriptionService : ISubsriptionService
{
    private readonly AppDbContext _context;

    public SubscriptionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateSubscription(PostCreateSubscriptionDto dto)
    {
        var checkClient = await _context.Clients
            .Include(client => client.Individual)
            .Include(client => client.Company)
            .FirstOrDefaultAsync(c => c.ClientId == dto.ClientId);

        if (checkClient == null)
        {
            throw new NotFoundException("Client with the provided id does not exist!");
        }
        
        var checkSoftVersion = await _context.SoftVersions.Include(softVersion => softVersion.Software).FirstOrDefaultAsync(sv => sv.SoftVersionId == dto.SoftVersionId);

        if (checkSoftVersion == null)
        {
            throw new NotFoundException("SoftVersion with the provided id does not exist!");
        }
        
        var hasActiveContract = await _context.Contracts.AnyAsync(
            c => c.ClientId == checkClient.ClientId 
                 && c.IsActive && c.SoftVersionId == checkSoftVersion.SoftVersionId);

        var hasActiveSubscription = await _context.Subscriptions.AnyAsync(
            s => s.ClientId == checkClient.ClientId &&
                 s.IsActive && s.SoftVersionId == checkSoftVersion.SoftVersionId
        );

        if (hasActiveContract || hasActiveSubscription)
        {
            throw new AlreadyDoneException("Contract or Subscription for specified client and software already exists and is Active!");
        }

        if (dto.BillingMonthsNumber < 1 || dto.BillingMonthsNumber > 24)
        {
            throw new NotPossibleException("Billing period must be between 1 and 24 months!");
        }

        var checkBillingPeriod = await  _context.BillingPeriods.FirstOrDefaultAsync(bp => bp.MonthsNumber == dto.BillingMonthsNumber);

        if (checkBillingPeriod == null){
            var name = dto.BillingMonthsNumber switch
            {
                24 => "Dwuletni",
                12 => "Roczny",
                6 => "Półroczny",
                1 => "Miesięczny",
                _ => "Custom"
            };

            checkBillingPeriod = new BillingPeriod
            {
                Type = name,
                MonthsNumber = dto.BillingMonthsNumber
            };
        
            await _context.BillingPeriods.AddAsync(checkBillingPeriod);
        }
        
        var timeNow = DateTime.Now;

        var bestDiscount = await _context.Discounts
            .OrderByDescending(d => d.Percentage)
            .FirstOrDefaultAsync(d =>
                d.StartDate <= timeNow && d.EndDate >= timeNow
            );
        
        var hadSubscription = await _context.Subscriptions.AnyAsync(s => s.ClientId == checkClient.ClientId);
        var hadPayedContract = await _context.Contracts.AnyAsync(c => c.ClientId == checkClient.ClientId && c.IsPaid);

        var periodPrice = Math.Round(checkSoftVersion.Software.LicensePricePerYear * (checkBillingPeriod.MonthsNumber/12m), 2);
        
        if (hadSubscription || hadPayedContract)
        {
            periodPrice -= periodPrice*0.05m;
        }

        var newSubscription = new Subscription
        {
            Client =  checkClient,
            SoftVersion =  checkSoftVersion,
            Name = dto.Name,
            BillingPeriod = checkBillingPeriod,
            PeriodPrice =  periodPrice,
            SubscriptionStartDate = timeNow,
            PeriodStartDate = timeNow,
            PeriodEndDate = timeNow.AddMonths(checkBillingPeriod.MonthsNumber),
            Discount = bestDiscount,
            IsClientLoyal = hadSubscription || hadPayedContract
        };
        _context.Subscriptions.Add(newSubscription);
        
        if (bestDiscount != null)
        {
            periodPrice -= periodPrice*(bestDiscount.Percentage / 100m);
        }

        var newSubscriptionPayment = new SubscriptionPayment
        {
            Subscription = newSubscription,
            Amount = periodPrice,
            PaymentDate = timeNow.AddMinutes(1)
        };
        
        _context.SubscriptionPayments.Add(newSubscriptionPayment);
        
        await _context.SaveChangesAsync();
    }

    public async Task AddSubscriptionPayment(int subscriptionId, PostSubscriptionPaymentDto dto)
    {
        var checkSubscription = await _context.Subscriptions
            .Include(subscription => subscription.BillingPeriod)
            .FirstOrDefaultAsync(s => s.SubscriptionId == subscriptionId && s.IsActive);
        if (checkSubscription == null)
        {
            throw new NotFoundException("Subscription with the provided id does not exist or is not active!");
        }

        if (checkSubscription.ClientId != dto.ClientId)
        {
            throw new NotPossibleException("This subscription belongs to other client!");
        }
        
        var maxPaymentDate = checkSubscription.PeriodEndDate.AddMonths(checkSubscription.BillingPeriod.MonthsNumber);
        var today = DateTime.Now;

        if (today > maxPaymentDate)
        {
            checkSubscription.IsActive = false;
            await _context.SaveChangesAsync();
            throw new AlreadyDoneException("You are late with payment! Subscription is canceled!");
        }

        if (today < checkSubscription.PeriodEndDate)
        {
            throw new AlreadyDoneException("You had already paid for this period!");
        }

        if (checkSubscription.PeriodPrice != dto.Amount)
        {
            throw new NotPossibleException("Your amount doesn't match subscription period price!");
        }

        var newSubscriptionPayment = new SubscriptionPayment
        {
            Subscription = checkSubscription,
            Amount = dto.Amount,
            PaymentDate = today
        };
        
        _context.SubscriptionPayments.Add(newSubscriptionPayment);
        
        checkSubscription.PeriodStartDate = checkSubscription.PeriodEndDate;
        checkSubscription.PeriodEndDate = checkSubscription.PeriodEndDate.AddMonths(checkSubscription.BillingPeriod.MonthsNumber);
        
        await _context.SaveChangesAsync();
    }
}