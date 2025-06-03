using Application.Model;
using Helper;

namespace Application.Abstraction;

public interface ICreditRequestServices
{
    Task<BaseResponse<CreateCerditRequestResponseModel>> CreateCreditRequest(CreateCerditRequestModel request, CancellationToken cancellationToken);
}
