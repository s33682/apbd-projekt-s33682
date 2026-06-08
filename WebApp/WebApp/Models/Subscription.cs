using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Models;

public class Subscription
{
    [Key]
    public int SubscriptionId { get; set; }
    
    public int ClientId { get; set; }
    [ForeignKey(nameof(ClientId))] 
    public Client Client { get; set; } = null!;
    
    public int SoftVersionId { get; set; }
    [ForeignKey(nameof(SoftVersionId))] 
    public SoftVersion SoftVersion { get; set; } = null!;

    [MaxLength(100)] 
    public string Name { get; set; } = null!;
    
    public int BillingPeriodId { get; set; }
    [ForeignKey(nameof(BillingPeriodId))] 
    public BillingPeriod BillingPeriod { get; set; } = null!;
    
    [Precision(10,2)]
    public decimal PeriodPrice { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public int? DiscountId { get; set; }
    [ForeignKey(nameof(DiscountId))]
    public Discount? Discount { get; set; }
    
    public bool IsClientLoyal { get; set; }
    public bool IsActive { get; set; } =  true;
    
    public ICollection<SubscriptionPayment> SubscriptionPayments { get; set; } = [];
}