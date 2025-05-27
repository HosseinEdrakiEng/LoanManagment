using System;
using System.Collections.Generic;

namespace Domain.Entites;

public partial class CreditPlan
{
    public long Id { get; set; }

    public string Title { get; set; } = null!;

    public bool Enable { get; set; }

    public byte ConfigTypeId { get; set; }

    public long CurrencyId { get; set; }

    public virtual ICollection<CreditPlanRequest> CreditPlanRequests { get; set; } = new List<CreditPlanRequest>();
}
