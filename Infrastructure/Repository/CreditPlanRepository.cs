using Application.Abstraction;
using Domain;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public async Task<CreditPlanModel> GetAsync(long id ,CancellationToken cancellationToken)
    {
        return await _dbContext.CreditPlans.AsNoTracking().SingleOrDefaultAsync(a=>a.Id == id,cancellationToken);
    }
}
