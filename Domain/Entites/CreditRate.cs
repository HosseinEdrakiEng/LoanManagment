using System;
using System.Collections.Generic;

namespace Domain.Entites;

public partial class CreditRate
{
    public long Id { get; set; }

    public string Range { get; set; } = null!;

    public virtual ICollection<CreditLimit> CreditLimits { get; set; } = new List<CreditLimit>();
}
