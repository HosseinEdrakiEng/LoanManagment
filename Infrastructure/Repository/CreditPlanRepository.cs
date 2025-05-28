using Application.Abstraction.IRepository;
using Application.Model;
using Domain.Entites;
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
    private readonly LoanManagmentDbContext _context;
    public CreditPlanRepository(LoanManagmentDbContext context)
    {
        _context = context;
    }

    public async Task<CreditPlan> GetByFilter(CreditPlanFilter filter , CancellationToken cancellationToken)
    {
        var query = _context.CreditPlans.AsQueryable();

        if (!string.IsNullOrEmpty(filter.Title))
            query.Where(a => a.Title == filter.Title);

        if (filter.CurrencyId>0)
            query.Where(a => a.CurrencyId == filter.CurrencyId);

        if (filter.ConfigTypeId > 0)
            query.Where(a => a.ConfigTypeId == filter.ConfigTypeId);

        if (filter.Enable is not null)
            query.Where(a => a.Enable == filter.Enable);

        return await query.AsNoTracking().FirstOrDefaultAsync(cancellationToken);
    }

}
