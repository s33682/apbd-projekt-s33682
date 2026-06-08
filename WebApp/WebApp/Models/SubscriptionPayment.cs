using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Models;

public class SubscriptionPayment
{
    [Key]
    public int PaymentId { get; set; }
    
    public int SubscriptionId { get; set; }
    [ForeignKey(nameof(SubscriptionId))] 
    public Subscription Subscription { get; set; } = null!;
    
    [Precision(10,2)]
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
}