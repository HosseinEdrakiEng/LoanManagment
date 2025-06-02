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
        private readonly ICreditPlanRepository _creditPlanRepository;
        private readonly IUserServices _userServices;

        public CreditPlanService(ICreditPlanRepository creditPlanRepository
            , IUserServices userServices)
        {
            _creditPlanRepository = creditPlanRepository;
            _userServices = userServices;
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

            var creditPlan = await _creditPlanRepository.LoadCreditPlans(request.GroupId, int.Parse(user.Data.Score), user.Data.Level, cancellationToken);
            if (creditPlan is null 
                || creditPlan.Count == 0)
            {
                response.Error = CustomErrors.UndefinedPlan;
                return response;
            }

            var result = creditPlan?.SelectMany(r => r.Limitations)?.ToList();
            response.Data = [.. result.Select(r => new LoadCreditPlanResponseModel 
            { 
                Amount = r.Amount,
                AnnualInterestRate = r.CreditPlan.AnnualInterestRate,
                CommissionCalculateType = r.CreditPlan.CommissionCalculateType,
                CommissionPercentage = r.CreditPlan.CommissionPercentage,
                ExpirationDate = r.CreditPlan.ExpirationDate,
                FineRate = r.CreditPlan.FineRate,
                GuarantyType = r.CreditPlan.GuarantyType,
                InstallmentCount = r.CreditPlan.InstallmentCount,
                InstalmentType = r.CreditPlan.InstalmentType,
                LoanType = r.CreditPlan.LoanType,
                PaymentType = r.CreditPlan.PaymentType,
                PrePaymentPercentage = r.CreditPlan.PrePaymentPercentage,
                RepaymentDeadline = r.CreditPlan.RepaymentDeadline,
                UsagePeriod = r.CreditPlan.UsagePeriod,
                Title = r.CreditPlan.Title,
                Id = r.CreditPlan.Id,
            })];

            return response;
        }
    }
}
