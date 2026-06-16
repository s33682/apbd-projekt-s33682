using WebApp.DTOs;

namespace WebApp.Services;

public interface IRevenueService
{
    public Task<GetRevenueDetailsDto> GetActualRevenue(int? softwareId, string? currency);
    public Task<GetRevenueDetailsDto> GetPredictedRevenue(int? softwareId, string? currency);
}