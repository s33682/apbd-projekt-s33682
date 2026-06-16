using Microsoft.EntityFrameworkCore;
using WebApp.Context;

namespace WebApp.Workers;

public class SubscriptionExpirationJob
{
    private readonly AppDbContext _context;
    
    public SubscriptionExpirationJob(AppDbContext context)
    {
        _context = context;
    }

    public async Task CancelExpiredSubscriptions()
    {
        var today = DateTime.Now;

        var expiredSubscriptions = await _context.Subscriptions
            .Where(s => s.IsActive && s.EndDate.AddMonths(s.BillingPeriod.MonthsNumber) < today)
            .ToListAsync();

        foreach (var expiredSubscription in expiredSubscriptions)
        {
            expiredSubscription.IsActive = false;
        }
        
        await _context.SaveChangesAsync();
    }
}