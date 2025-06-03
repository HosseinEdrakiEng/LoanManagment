using Domain;

namespace Application.Abstraction;

public interface ICreditRequestRepository
{
    Task AddAsync(CreditRequestModel model, CancellationToken cancellationToken);
}
