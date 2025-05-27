using Domain.Entites;

namespace Application.Abstraction.IRepository
{
    public interface INotRegisterationCreditPlanRequestRepository
    {
        Task Insert(NotRegisterationCreditPlanRequest model, CancellationToken cancellationToken);
        Task BulkInsert(List<NotRegisterationCreditPlanRequest> models, CancellationToken cancellationToken);
    }
}
