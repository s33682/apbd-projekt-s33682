using Microsoft.EntityFrameworkCore;
using WebApp.Context;
using WebApp.DTOs;
using WebApp.Exceptions;
using WebApp.Models;

namespace WebApp.Services;

public class ContractService : IContractService
{
    private readonly AppDbContext _context;

    public ContractService(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateContract(PostNewContractDto dto)
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
        
        var today = DateOnly.FromDateTime(DateTime.Now);
        if (today.AddDays(3) > dto.MaximumPaymentDate || dto.MaximumPaymentDate > today.AddDays(30))
        {
            throw new NotPossibleException("Maximum payment date must be between 3 and 30 days!");
        }
        
        var timeNow = DateTime.Now;

        var bestDiscount = await _context.Discounts
            .OrderByDescending(d => d.Percentage)
            .FirstOrDefaultAsync(d =>
            d.StartDate <= timeNow && d.EndDate >= timeNow
        );
        
        var hadSubscription = await _context.Subscriptions.AnyAsync(s => s.ClientId == checkClient.ClientId);
        var hadPayedContract = await _context.Contracts.AnyAsync(c => c.ClientId == checkClient.ClientId && c.IsPaid);

        var fullPrice = checkSoftVersion.Software.LicensePricePerYear + 1000*(dto.AdditionalSupportYears ?? 0);

        if (bestDiscount != null)
        {
            fullPrice -= fullPrice*(bestDiscount.Percentage / 100m);
        }

        if (hadSubscription || hadPayedContract)
        {
            fullPrice -= fullPrice*0.05m;
        }

        var newContract = new Contract
        {
            Client = checkClient,
            SoftVersion = checkSoftVersion,
            MinimumPaymentDate = today,
            MaximumPaymentDate = dto.MaximumPaymentDate,
            Discount = bestDiscount,
            IsClientLoyal = hadSubscription||hadPayedContract,
            AdditionalSupportYears = dto.AdditionalSupportYears,
            FullPrice = fullPrice,
        };
        
        await _context.Contracts.AddAsync(newContract);
        await _context.SaveChangesAsync();
    }

    public async Task AddContractPayment(int contractId, PostContractPaymentDto dto)
    {
        var checkContract = await _context.Contracts.FirstOrDefaultAsync(c => c.ContractId == contractId
        && c.IsActive &&  !c.IsPaid
        );

        if (checkContract == null)
        {
            throw new NotFoundException("Contract with the provided id does not exist, is unactive or already paid!");
        }

        if (checkContract.ClientId != dto.ClientId)
        {
            throw  new NotPossibleException("This contract belongs to other client!");
        }

        var howMuchAlreadyPaid = await _context.ContractPayments
            .Where(cp => cp.Contract == checkContract && !cp.IsRefunded).SumAsync(cp => cp.Amount);
        
        var leftToPay = checkContract.FullPrice - howMuchAlreadyPaid;
        
        var today = DateOnly.FromDateTime(DateTime.Now);
        
        if (today > checkContract.MaximumPaymentDate)
        {
            checkContract.IsActive = false;

            if (howMuchAlreadyPaid > 0)
            {
                var payments = await _context.ContractPayments
                    .Where(cp => cp.Contract == checkContract && !cp.IsRefunded).ToListAsync();

                foreach (var payment in payments)
                {
                    payment.IsRefunded = true;
                }
            }
            await _context.SaveChangesAsync();

            throw new NotPossibleException("You are late with payment!");
        }
        
        if (leftToPay < dto.Amount)
        {
            throw new NotPossibleException($"You want to pay too much! You need to pay just {leftToPay}$!");
        }

        var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var newPayment = new ContractPayment
            {
                Contract = checkContract,
                Amount = dto.Amount,
                CreatedAt = DateTime.Now
            };

            await _context.ContractPayments.AddAsync(newPayment);
            await _context.SaveChangesAsync();

            if (dto.Amount == leftToPay)
            {
                checkContract.IsPaid = true;
                await _context.SaveChangesAsync();
            }
            
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}