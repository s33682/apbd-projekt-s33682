using Microsoft.EntityFrameworkCore;
using WebApp.Context;

namespace WebApp.Workers;

public class ContractExpirationJob
{
    private readonly AppDbContext _context;
    
    public ContractExpirationJob(AppDbContext context)
    {
        _context = context;
    }

    public async Task CancelExpiredContracts()
    {
        var today = DateOnly.FromDateTime(DateTime.Now);

        var expiredContracts = await _context.Contracts
            .Include( c => c.Payments)
            .Where(c => c.IsActive && !c.IsPaid && c.MaximumPaymentDate < today)
            .ToListAsync();

        foreach (var expiredContract in expiredContracts)
        {
            expiredContract.IsActive = false;
            
            var paymentsToRefund = expiredContract.Payments.Where(cp => !cp.IsRefunded);

            foreach (var payment in paymentsToRefund)
            {
                payment.IsRefunded = true;
            }
        }
        
        await _context.SaveChangesAsync();
    }
}