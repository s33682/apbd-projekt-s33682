namespace WebApp.DTOs;

public class GetRevenueDetailsDto
{
    public decimal Amount { get; set; }
    public string Currency { get; set; } = String.Empty;
    public int? SoftwareId { get; set; }
}