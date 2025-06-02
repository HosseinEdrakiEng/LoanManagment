using Domain.Entites;
using System;
using System.Collections.Generic;

namespace Domain;

public class CreditLevelModel : BaseEntity
{

    public string Title { get; set; }

    public ICollection<LimitationModel> Limitations { get; set; } = new List<LimitationModel>();
}
