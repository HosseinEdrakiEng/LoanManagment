using Application.Abstraction;
using Application.Model;
using Domain;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class LimitationRepository : ILimitationRepository
    {
        private readonly LoanManagmentDbContext _dbContext;
        public LimitationRepository(LoanManagmentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<LimitationModel> GetAsync(LimitationFilterModel filterModel, CancellationToken cancellationToken)
        {
            return await _dbContext.Limitations.Include(a => a.CreditLevel).AsNoTracking()
                                    .Where(a => a.CreditPlanId == filterModel.PlanId)
                                    .Where(a => a.Score == filterModel.Score)
                                    .Where(a => a.CreditLevel.Title == filterModel.Level)
                                    .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<LimitationModel>> LoadLimitationCreditPlans(long groupId, int score, string level, CancellationToken cancellationToken)
        {
            var result = await _dbContext.CreditPlans
              .AsNoTracking()
              .Where(plan => plan.GroupId == groupId && !plan.IsDeleted && plan.IsActive)
              .SelectMany(
                  plan => plan.Limitations.Where(lim => lim.Score == score && lim.CreditLevel.Title == level),
                  (plan, lim) => new LimitationModel { CreditPlan = plan, Amount = lim.Amount }
              )
              .ToListAsync(cancellationToken);

            return result;
        }
    }
}
