using Application.Model;
using Domain;
using Helper;

namespace Application.Abstraction;

public interface ICreditRequestStrategy
{
    Task<BaseResponse<CreateCerditRequestResponseModel>> ProcessAsync(CreateCerditRequestModel request, CreditPlanModel creditPlan, CancellationToken cancellationToken);
}


