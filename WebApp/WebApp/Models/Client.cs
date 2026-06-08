using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models;

public class Client
{
    [Key]
    public int ClientId { get; set; }
    public int? IndividualId { get; set; }
    [ForeignKey(nameof(IndividualId))] 
    public Individual? Individual { get; set; }
    
    public int? CompanyId { get; set; }
    [ForeignKey(nameof(CompanyId))]
    public Company? Company { get; set; }
    
    public ICollection<Contract> Contracts { get; set; } = [];
    public ICollection<Subscription> Subscriptions { get; set; } = [];
}