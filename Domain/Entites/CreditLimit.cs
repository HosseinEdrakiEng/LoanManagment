using System;
using System.Collections.Generic;

namespace Domain.Entites;

public partial class CreditLimit
{
    public long Id { get; set; }

    public byte ConfigTypeId { get; set; }

    public long CreditRateId { get; set; }

    public long CreditLevelId { get; set; }

    public long Limit { get; set; }

    public long CurrencyId { get; set; }

    public virtual CreditLevel CreditLevel { get; set; } = null!;

    public virtual CreditRate CreditRate { get; set; } = null!;

    public virtual ICollection<UserLimit> UserLimits { get; set; } = new List<UserLimit>();
}
