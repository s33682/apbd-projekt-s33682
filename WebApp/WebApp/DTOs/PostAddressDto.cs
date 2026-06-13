using System.ComponentModel.DataAnnotations;

namespace WebApp.DTOs;

public class PostAddressDto
{
    [MaxLength(50)]
    public string City { get; set; } = String.Empty;
    [RegularExpression("^[0-9]{2}-[0-9]{3}$",  ErrorMessage = "Invalid zip code format!")]
    [StringLength(6, MinimumLength = 6)]
    public string ZipCode { get; set; } = String.Empty;
    [MaxLength(100)]
    public string Street { get; set; } = String.Empty;
    [MaxLength(10)]
    public string Number { get; set; } = String.Empty;
    [MaxLength(10)]
    public string? Apartment { get; set; }
}