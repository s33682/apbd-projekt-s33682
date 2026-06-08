using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class Address
{
    [Key]
    public int AddressId { get; set; }
    [MaxLength(50)]
    public string City { get; set; } = String.Empty;
    [MaxLength(6)]
    public string ZipCode { get; set; } = String.Empty;
    [MaxLength(100)]
    public string Street { get; set; } = String.Empty;
    [MaxLength(10)]
    public string Number { get; set; } = String.Empty;
    [MaxLength(10)] 
    public string? Apartment { get; set; } = String.Empty;

    public ICollection<Individual> IndividualClients { get; set; } = [];
    public ICollection<Company> CompanyClients { get; set; } = [];
}