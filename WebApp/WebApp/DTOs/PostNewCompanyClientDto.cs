using System.ComponentModel.DataAnnotations;

namespace WebApp.DTOs;

public class PostNewCompanyClientDto
{
    [MaxLength(100)]
    public string Name { get; set; } = String.Empty;
    public PostAddressDto Address { get; set; } = new();
    [MaxLength(150)]
    [EmailAddress]
    public string Email { get; set; } = String.Empty;
    [StringLength(9, MinimumLength = 9)]
    [RegularExpression("^[0-9]{9}$",  ErrorMessage = "Number must be just 9 digits!")]
    public string PhoneNumber { get; set; } = String.Empty;
    [StringLength(10, MinimumLength = 10)]
    [RegularExpression("^[0-9]{10}$",  ErrorMessage = "Krs number must be just 10 digits!")]
    public string KrsNumber { get; set; } = String.Empty;
}