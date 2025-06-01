using Domain.Entites;
using System;
using System.Collections.Generic;

namespace Domain;

public class CreditLevelModel : BaseEntity
{

    public string Title { get; set; }

    public virtual ICollection<LimitationModel> Limitations { get; set; } = new List<LimitationModel>();
}
