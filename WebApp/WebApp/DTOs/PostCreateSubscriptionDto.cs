using System.ComponentModel.DataAnnotations;

namespace WebApp.DTOs;

public class PostCreateSubscriptionDto
{
    public int ClientId { get; set; }
    public int SoftVersionId { get; set; }
    [MaxLength(100)] 
    public string Name { get; set; } = String.Empty;
    public int BillingPeriodId { get; set; }
    public int? DiscountId { get; set; }
}