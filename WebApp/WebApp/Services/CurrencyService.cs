using WebApp.DTOs;
using WebApp.Exceptions;

namespace WebApp.Services;

public class CurrencyService : ICurrencyService
{
    private readonly HttpClient _httpClient;
    
    public CurrencyService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<decimal> GetRateAsync(string currency)
    {
        var rate = await _httpClient.GetFromJsonAsync<GetExchangeRateDto>($"https://api.nbp.pl/api/exchangerates/rates/A/{currency}/?format=json");

        if (rate == null)
        {
            throw new NotPossibleException("You provided wrong currency code!");
        }

        return rate.Rates[0].Mid;
    }
}