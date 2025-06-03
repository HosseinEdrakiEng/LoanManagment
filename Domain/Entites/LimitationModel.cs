using Domain.Entites;

namespace Domain;

public class LimitationModel : BaseEntity
{
    public int Score { get; set; }

    public long CreditLevelId { get; set; }

    public long CreditPlanId { get; set; }

    public long Amount { get; set; }

    public CreditLevelModel CreditLevel { get; set; } = null!;

    public CreditPlanModel CreditPlan { get; set; } = null!;

    public ICollection<CreditRequestModel> CreditRequests { get; set; } = new List<CreditRequestModel>();
}
