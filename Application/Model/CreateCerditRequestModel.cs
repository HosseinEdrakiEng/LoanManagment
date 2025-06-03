namespace Application.Model;

public class CreateCerditRequestModel
{
    public long PlanId { get; set; }
    public string UserId { get; set; }
    public string PhoneNumber { get; set; }
    public string NationalCode { get; set; }
    public long Amount { get; set; }
}
