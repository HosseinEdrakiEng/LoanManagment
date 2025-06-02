using Domain;

namespace Application.Abstraction;

public interface ICreditPlanRepository
{
    Task<CreditPlanModel> GetAsync(long id, CancellationToken cancellationToken);
    Task<List<CreditPlanModel>> LoadCreditPlans(long groupId, int score, string level, CancellationToken cancellationToken);
}
