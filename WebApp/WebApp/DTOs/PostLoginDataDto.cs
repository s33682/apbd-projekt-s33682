using System.ComponentModel.DataAnnotations;

namespace WebApp.DTOs;

public class PostLoginDataDto
{
    [Required]
    [MaxLength(20)]
    public string Login { get; set; }  = String.Empty;
    [Required]
    [MaxLength(32)]
    public string Password { get; set; } = String.Empty;
}