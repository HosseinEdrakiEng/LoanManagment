using Application.Abstraction;
using Application.Model;
using Domain;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class LimitationRepository : ILimitationRepository
    {
        private readonly LoanManagmentDbContext _dbContext;
        public LimitationRepository(LoanManagmentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<LimitationModel> GetAsync(LimitationFilterModel filterModel , CancellationToken cancellationToken)
        {
            return  await _dbContext.Limitations.Include(a=>a.CreditLevel).AsNoTracking()
                                    .Where(a => a.CreditPlanId == filterModel.PlanId)
                                    .Where(a => a.Score == filterModel.Score)
                                    .Where(a => a.CreditLevel.Title == filterModel.Level)
                                    .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
