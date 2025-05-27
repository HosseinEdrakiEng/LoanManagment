using System;
using System.Collections.Generic;

namespace Domain.Entites;

public partial class UserLimit
{
    public long Id { get; set; }

    public string UserId { get; set; } = null!;

    public long CreditLimitId { get; set; }

    public virtual CreditLimit CreditLimit { get; set; } = null!;
}
