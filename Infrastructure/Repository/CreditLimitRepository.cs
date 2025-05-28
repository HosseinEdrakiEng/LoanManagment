using Application.Abstraction.IRepository;
using Domain.Entites;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class CreditLimitRepository : ICreditLimitRepository
    {
        private readonly LoanManagmentDbContext _context;
        public CreditLimitRepository(LoanManagmentDbContext context)
        {
            _context = context;
        }

        public IQueryable<CreditLimit> GetQueryable()
        {
            return _context.CreditLimits.AsNoTracking()
                .Include(a => a.CreditRate)
                .Include(b => b.CreditLevel)
                .Include(b => b.UserLimits);
        }
    }
}
