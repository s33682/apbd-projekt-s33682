using System.ComponentModel.DataAnnotations;

namespace WebApp.DTOs;

public class GetExampleDto
{
    [Required]
    public  int Id { get; set; }
    [MaxLength(50)]
    public string Name { get; set; } = String.Empty;
}