using System;
using System.Collections.Generic;

namespace Domain.Entites;

public partial class CreditPlanRequest
{
    public long Id { get; set; }

    public DateTime CreateTime { get; set; }

    public long CreditPlanId { get; set; }

    public string UserId { get; set; } = null!;

    public byte Status { get; set; }

    public virtual CreditPlan CreditPlan { get; set; } = null!;
}
