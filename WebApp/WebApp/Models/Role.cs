using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class Role
{
    [Key]
    public int RoleId { get; set; }
    [MaxLength(50)]
    public string RoleName { get; set; } = String.Empty;
    
    public ICollection<Employee> Employees { get; set; } = [];
}