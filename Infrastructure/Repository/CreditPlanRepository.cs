using Application.Abstraction;
using Domain;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class CreditPlanRepository : ICreditPlanRepository
{
    private readonly LoanManagmentDbContext _dbContext;

    public CreditPlanRepository(LoanManagmentDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<CreditPlanModel>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.CreditPlans.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<CreditPlanModel> GetAsync(long id, CancellationToken cancellationToken)
    {
        return await _dbContext.CreditPlans.AsNoTracking().SingleOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

    public async Task<List<CreditPlanModel>> LoadCreditPlans(long groupId, int score, string level, CancellationToken cancellationToken)
    {
        var t = await _dbContext.CreditPlans.AsNoTracking()
                .Include(r => r.Limitations)
                    .ThenInclude(r => r.CreditLevel)
                .Where(a => a.GroupId == groupId
                    && !a.IsDeleted
                    && a.IsActive
                    && a.Limitations.Any(r => r.CreditLevel.Title == level && r.Score == score))
                .ToQueryString();


        var plans = await _dbContext.CreditPlans.AsNoTracking()
                        .Include(r => r.Limitations)
                            .ThenInclude(r => r.CreditLevel)
                        .Where(a => a.GroupId == groupId
                            && !a.IsDeleted
                            && a.IsActive
                            && a.Limitations.Any(r => r.CreditLevel.Title == level && r.Score == score))
                        .ToListAsync(cancellationToken);

        return plans;
    }
}