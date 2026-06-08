using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class DiscountType
{
    [Key]
    public int DiscountTypeId { get; set; }
    [MaxLength(100)]
    public string Offer { get; set; } = String.Empty;
    
    public ICollection<Discount> Discounts { get; set; } = [];
}