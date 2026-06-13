using System.ComponentModel.DataAnnotations;

namespace WebApp.DTOs;

public class PostNewIndividualClientDto
{
   [MaxLength(50)]
   public string FirstName { get; set; } = String.Empty;
   [MaxLength(50)]
   public string LastName { get; set; } = String.Empty;
   public PostAddressDto Address { get; set; } = new();
   [MaxLength(150)]
   [EmailAddress]
   public string Email { get; set; } = String.Empty;
   [StringLength(9, MinimumLength = 9)]
   [RegularExpression("^[0-9]{9}$",  ErrorMessage = "Number must be just 9 digits!")]
   public string PhoneNumber { get; set; } = String.Empty;
   [StringLength(11, MinimumLength = 11)]
   [RegularExpression("^[0-9]{11}$",  ErrorMessage = "Pesel must be just 11 digits!")]
   public string Pesel { get; set; } = String.Empty;
}