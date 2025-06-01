using Application.Model;
using Helper;

namespace Application.Abstraction;
public interface ICreditPlanRequestService
{
    Task<BaseResponse<PreRegsitrationResponseModel>> PreRegsitration(PreRegsitrationRequestModel request, CancellationToken cancellationToken);
}
