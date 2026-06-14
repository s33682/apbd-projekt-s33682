using System.ComponentModel.DataAnnotations;

namespace WebApp.DTOs;

public class PostContractPaymentDto
{
    public int ClientId { get; set; }
    [Range(0.01, 99999.99)]
    public decimal Amount { get; set; }
}