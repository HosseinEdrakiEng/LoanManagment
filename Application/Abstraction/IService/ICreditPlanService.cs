using Application.Model;
using Helper;

namespace Application.Abstraction.IService
{
    public interface ICreditPlanService
    {
        Task<BaseResponse<List<LoadCreditPlanResponseModel>>> LoadCreditPlan(LoadCreditPlanRequestModel request, CancellationToken cancellationToken);
    }
}
