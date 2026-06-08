using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Models;

[Index(nameof(Pesel), IsUnique = true)]
public class Individual
{
    [Key]
    public int IndividualId { get; set; }
    [MaxLength(50)]
    public string FirstName { get; set; } = String.Empty;
    [MaxLength(50)]
    public string LastName { get; set; } = String.Empty;
    
    public int AddressId { get; set; }
    [ForeignKey(nameof(AddressId))] 
    public Address Address { get; set; } = null!;
    
    [MaxLength(150)]
    public string Email { get; set; } = String.Empty;
    [Precision(9,0)]
    public decimal PhoneNumber { get; set; }
    [MaxLength(11)] 
    public string Pesel { get; set; } = String.Empty;
    public bool IsActive { get; set; } = true;
    
    public ICollection<Client> Client { get; set; } = [];
}