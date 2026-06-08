using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Models;

public class ContractPayment
{
    [Key]
    public int PaymentId { get; set; }
    
    public int ContractId { get; set; }
    [ForeignKey(nameof(ContractId))] 
    public Contract Contract { get; set; } = null!;

    [Precision(10,2)]
    public decimal Amount { get; set; }
    public bool IsRefunded { get; set; }
    public DateTime CreatedAt { get; set; }
}