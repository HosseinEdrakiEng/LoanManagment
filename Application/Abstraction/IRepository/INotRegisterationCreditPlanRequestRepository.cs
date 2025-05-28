using Application.Common;
using Domain.Entites;

namespace Application.Abstraction.IRepository
{
    public interface INotRegisterationCreditPlanRequestRepository
    {
        Task Insert(NotRegisterationCreditPlanRequest model, CancellationToken cancellationToken);
        Task BulkInsert(List<NotRegisterationCreditPlanRequest> models, CancellationToken cancellationToken);
        Task<List<NotRegisterationCreditPlanRequest>> GetByNoneStatus(CancellationToken cancellationToken);

        Task UpdateStatus(List<long> ids, NotRegisterationStatusType status, CancellationToken cancellationToken);
    }
}
