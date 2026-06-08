using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class BillingPeriod
{
    [Key]
    public int PeriodId { get; set; }
    [MaxLength(50)]
    public string Type { get; set; } = String.Empty;
    public int MonthsNumber { get; set; }
    
    public ICollection<Subscription> Subscriptions { get; set; } = [];
}