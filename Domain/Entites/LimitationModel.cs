using Domain.Entites;
using System;
using System.Collections.Generic;

namespace Domain;

public class LimitationModel :BaseEntity
{
    public int Score { get; set; }

    public long CreditLevelId { get; set; }

    public long CreditPlanId { get; set; }

    public long Amount { get; set; }

    public virtual CreditLevelModel CreditLevel { get; set; } = null!;

    public virtual CreditPlanModel CreditPlan { get; set; } = null!;
}
