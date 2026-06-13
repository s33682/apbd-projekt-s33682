using System.ComponentModel.DataAnnotations;

namespace WebApp.DTOs;

public class PutUpdateIndividualClientDto
{
    [MaxLength(50)]
    public string? FirstName { get; set; }
    [MaxLength(50)]
    public string? LastName { get; set; }
    public PostAddressDto? Address { get; set; }
    [MaxLength(150)]
    [EmailAddress]
    public string? Email { get; set; }
    [StringLength(9, MinimumLength = 9)]
    [RegularExpression("^[0-9]{9}$",  ErrorMessage = "Number must be just 9 digits!")]
    public string? PhoneNumber { get; set; }
}