using Domain.Entites;

namespace Domain;

public class CreditRequestModel : BaseEntity
{
    public long CreditPlanId { get; set; }

    public long LimitationId { get; set; }

    public Guid UserId { get; set; }

    public long Amount { get; set; }

    public byte Status { get; set; }

    public ICollection<CreditLoanModel> CreditLoans { get; set; } = new List<CreditLoanModel>();

    public CreditPlanModel CreditPlan { get; set; } = null!;

    public LimitationModel Limitation { get; set; } = null!;
}
