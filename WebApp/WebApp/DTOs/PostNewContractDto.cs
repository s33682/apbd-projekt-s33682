namespace WebApp.DTOs;

public class PostNewContractDto
{
    public int ClientId { get; set; }
    public int SoftVersionId { get; set; }
    public DateOnly MaximumPaymentDate { get; set; }
    public int? AdditionalSupportYears { get; set; }
}