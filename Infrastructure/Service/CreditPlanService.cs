using Application.Abstraction;
using Application.Abstraction.IService;
using Application.Common;
using Application.Model;
using Helper;
using Mapster;

namespace Infrastructure.Service
{
    public class CreditPlanService : ICreditPlanService
    {
        private readonly ILimitationRepository _limitationRepository;
        private readonly IUserServices _userServices;
        private readonly IInquiryServices _inquiryServices;

        public CreditPlanService(ILimitationRepository limitationRepository
            , IUserServices userServices
            , IInquiryServices inquiryServices)
        {
            _limitationRepository = limitationRepository;
            _userServices = userServices;
            _inquiryServices = inquiryServices;
        }

        public async Task<BaseResponse<List<LoadCreditPlanResponseModel>>> LoadCreditPlan(LoadCreditPlanRequestModel request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<List<LoadCreditPlanResponseModel>>();

            var user = await _userServices.Profile(request.PhoneNumber, cancellationToken);
            if (user.HasError)
            {
                response.Error = user.Error;
                return response;
            }

            var personScore = await _inquiryServices.PersonScore(request.NationalCode, cancellationToken);
            if (personScore.HasError)
            {
                response.Error = personScore.Error;
                return response;
            }

            var creditPlan = await _limitationRepository.LoadLimitationCreditPlans(request.GroupId, personScore.Data.Score.Value, user.Data.Level, cancellationToken);
            if (creditPlan is null
                || creditPlan.Count == 0)
            {
                response.Error = CustomErrors.UndefinedPlan;
                return response;
            }

            response.Data = creditPlan.Adapt<List<LoadCreditPlanResponseModel>>();
            return response;
        }
    }
}
