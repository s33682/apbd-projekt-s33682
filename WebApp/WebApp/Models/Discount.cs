using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Models;

public class Discount
{
    [Key]
    public int DiscountId { get; set; }
    [MaxLength(100)]
    public string Name { get; set; } = String.Empty;
    
    public int DiscountTypeId { get; set; }
    [ForeignKey(nameof(DiscountTypeId))] 
    public DiscountType DiscountType { get; set; } = null!;
    [Precision(3,0)]
    public decimal Percentage { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public ICollection<Contract> Contracts { get; set; } = [];
    public ICollection<Subscription> Subscriptions { get; set; } = [];
}