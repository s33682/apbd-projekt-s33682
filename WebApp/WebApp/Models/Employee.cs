using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models;

public class Employee
{
    [Key]
    public int EmployeeId { get; set; }
    [MaxLength(20)]
    public string Login { get; set; } = String.Empty;
    [MaxLength(255)]
    public string Password { get; set; } = String.Empty;
    
    public int RoleId { get; set; }
    [ForeignKey(nameof(RoleId))] 
    public Role Role { get; set; } = null!;
}