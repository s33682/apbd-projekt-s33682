using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class SoftCategory
{
    [Key]
    public int SoftCategoryId { get; set; }
    [MaxLength(50)]
    public string Name { get; set; } = String.Empty;
    
    public ICollection<Software> Softwares { get; set; } = [];
}