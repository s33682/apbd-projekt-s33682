using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Models;

[Index(nameof(KrsNumber), IsUnique = true)]
public class Company
{
    [Key]
    public int CompanyId { get; set; }
    [MaxLength(100)]
    public string Name { get; set; } = String.Empty;
    
    public int AddressId { get; set; }
    [ForeignKey(nameof(AddressId))] 
    public Address Address { get; set; } = null!;
    
    [MaxLength(150)]
    public string Email { get; set; } = String.Empty;
    [Precision(9,0)]
    public decimal PhoneNumber { get; set; }
    [MaxLength(10)]
    public string KrsNumber { get; set; } = String.Empty;
    
    public ICollection<Client>  Client { get; set; } = new List<Client>();
}