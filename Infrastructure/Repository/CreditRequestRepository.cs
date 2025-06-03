using Application.Abstraction;
using Domain;
using Infrastructure.Persistence;

namespace Infrastructure.Repository;

public class CreditRequestRepository : ICreditRequestRepository
{
    private readonly LoanManagmentDbContext _dbContext;
    public CreditRequestRepository(LoanManagmentDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(CreditRequestModel model, CancellationToken cancellationToken)
    {
        await _dbContext.CreditRequests.AddAsync(model, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

}
