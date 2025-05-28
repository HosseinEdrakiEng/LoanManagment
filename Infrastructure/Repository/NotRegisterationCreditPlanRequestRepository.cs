using Application.Abstraction.IRepository;
using Application.Common;
using Domain.Entites;
using EFCore.BulkExtensions;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class NotRegisterationCreditPlanRequestRepository : INotRegisterationCreditPlanRequestRepository
    {
        private readonly LoanManagmentDbContext _context;

        public NotRegisterationCreditPlanRequestRepository(LoanManagmentDbContext context)
        {
            _context = context;
        }

        public async Task Insert(NotRegisterationCreditPlanRequest model, CancellationToken cancellationToken)
        {
            _context.NotRegisterationCreditPlanRequests.Add(model);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task BulkInsert(List<NotRegisterationCreditPlanRequest> models, CancellationToken cancellationToken)
        {
            _context.NotRegisterationCreditPlanRequests.AddRange(models);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<NotRegisterationCreditPlanRequest>> GetByNoneStatus(CancellationToken cancellationToken)
        {
            return await _context.NotRegisterationCreditPlanRequests.AsNoTracking().Where(a => a.Status == (byte)NotRegisterationStatusType.None).ToListAsync(cancellationToken);
        }

        public async Task UpdateStatus(List<long> ids, NotRegisterationStatusType status, CancellationToken cancellationToken)
        {
            await _context.NotRegisterationCreditPlanRequests
                .Where(a => ids.Contains(a.Id))
                .ExecuteUpdateAsync(setters => setters.SetProperty(r => r.Status, (byte)status), cancellationToken);
        }
    }
}
