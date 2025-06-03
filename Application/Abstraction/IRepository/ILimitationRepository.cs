using Application.Model;
using Domain;

namespace Application.Abstraction;

public interface ILimitationRepository
{
    Task<LimitationModel> GetAsync(LimitationFilterModel filterModel, CancellationToken cancellationToken);
    Task<List<LimitationModel>> LoadLimitationCreditPlans(long groupId, int score, string level, CancellationToken cancellationToken);
}
