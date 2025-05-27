using Application.Model;
using Helper;

namespace Application.Abstraction.IService
{
    public interface ICreditPlanRequestService
    {
        Task<BaseResponse<PreRegsitrationResponseModel>> PreRegsitration(PreRegsitrationRequestModel request, CancellationToken cancellationToken);
    }
}
