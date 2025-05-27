using Application.Abstraction.IRepository;
using Domain.Entites;
using Infrastructure.Persistence;

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
    }
}
