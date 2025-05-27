using System;
using System.Collections.Generic;

namespace Domain.Entites;

public partial class CreditLevel
{
    public long Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<CreditLimit> CreditLimits { get; set; } = new List<CreditLimit>();
}
