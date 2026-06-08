using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Models;

public class Software
{
    [Key]
    public int SoftwareId { get; set; }
    [MaxLength(100)]
    public string Name { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    
    public int SoftCategoryId { get; set; }
    [ForeignKey(nameof(SoftCategoryId))] 
    public SoftCategory SoftCategory { get; set; } = null!;
    
    [Precision(10,2)]
    public decimal LicensePricePerYear { get; set; }
    
    public ICollection<SoftVersion> SoftVersions { get; set; } = [];
}