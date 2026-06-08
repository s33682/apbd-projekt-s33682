using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models;

public class SoftVersion
{
    [Key]
    public int SoftVersionId { get; set; }
    [MaxLength(10)]
    public string VersionNumber { get; set; } = String.Empty;
    public bool IsNewest { get; set; }
    
    public int SoftwareId { get; set; }
    [ForeignKey(nameof(SoftwareId))] 
    public Software Software { get; set; } = null!;
    
    public ICollection<Contract> Contracts { get; set; } = [];
    public ICollection<Subscription> Subscriptions { get; set; } = [];
}