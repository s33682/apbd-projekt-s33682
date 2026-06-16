namespace WebApp.Services;

public interface ICurrencyService
{
    public Task<decimal> GetRateAsync(string currency);
}