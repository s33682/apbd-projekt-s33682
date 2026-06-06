using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class Example
{
    [Key]
    public int ExampleId { get; set; }
    
    public string Name { get; set; } = String.Empty;
    
    public ICollection<ExampleModel> ExampleModels { get; set; } = new List<ExampleModel>();
}