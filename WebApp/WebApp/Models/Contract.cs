using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Models;

public class Contract
{
    [Key]
    public int ContractId { get; set; }
    
    public int ClientId { get; set; }
    [ForeignKey(nameof(ClientId))] 
    public Client Client { get; set; } = null!;
    
    public int SoftVersionId { get; set; }
    [ForeignKey(nameof(SoftVersionId))] 
    public SoftVersion SoftVersion { get; set; } = null!;
    
    public DateOnly MinimumPaymentDate { get; set; }
    public DateOnly MaximumPaymentDate { get; set; }
    public bool IsActive { get; set; } =  true;
    
    public int? DiscountId { get; set; }
    [ForeignKey(nameof(DiscountId))]
    public Discount? Discount { get; set; }
    
    public bool IsClientLoyal { get; set; }
    public int? AdditionalSupportYears { get; set; }
    public bool IsPaid { get; set; } = false;
    [Precision(10,2)]
    public decimal FullPrice { get; set; }
    
    public ICollection<ContractPayment> Payments { get; set; } = [];
}