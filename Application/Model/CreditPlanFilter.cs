namespace Application.Model;

public class CreditPlanFilter
{
    public string? Title { get; set; }

    public bool? Enable { get; set; }

    public byte? ConfigTypeId { get; set; }

    public long? CurrencyId { get; set; }
}
