using Domain.Entites;

namespace Domain;

public class CreditLevelModel : BaseEntity
{

    public string Title { get; set; }

    public ICollection<LimitationModel> Limitations { get; set; } = new List<LimitationModel>();
}
