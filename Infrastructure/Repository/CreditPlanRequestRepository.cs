using Application.Abstraction.IRepository;
using Domain.Entites;
using Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class CreditPlanRequestRepository : ICreditPlanRequestRepository
    {
        private readonly LoanManagmentDbContext _context;
        public CreditPlanRequestRepository(LoanManagmentDbContext context)
        {
            _context = context;
        }

        public async Task Insert(CreditPlanRequest model, CancellationToken cancellationToken)
        {
            _context.CreditPlanRequests.Add(model);
            await _context.SaveChangesAsync(cancellationToken);
        }

      
    }
}
