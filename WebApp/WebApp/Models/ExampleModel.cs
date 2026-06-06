using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models;

public class ExampleModel
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public int ExampleId { get; set; }

    [ForeignKey(nameof(ExampleId))] 
    public Example Example { get; set; } = null!;

}