using Application.Abstraction;
using Application.Common;
using Application.Model;
using Domain;
using Helper;
using Mapster;

namespace Infrastructure.Service
{
    public class LoanProcessingStrategy : ICreditRequestStrategy
    {
        private readonly IWalletServices _walletServices;
        private readonly IUserServices _userServices;
        private readonly IInquiryServices _inquiryServices;
        private readonly ILimitationRepository _limitationRepository;
        private readonly ICreditRequestRepository _creditRequestRepository;

        public LoanProcessingStrategy(IWalletServices walletServices, IUserServices userServices, IInquiryServices inquiryServices,
            ILimitationRepository limitationRepository, ICreditRequestRepository creditRequestRepository)
        {
            _walletServices = walletServices;
            _limitationRepository = limitationRepository;
            _creditRequestRepository = creditRequestRepository;
            _userServices = userServices;
            _inquiryServices = inquiryServices;
        }

        public async Task<BaseResponse<CreateCerditRequestResponseModel>> ProcessAsync(CreateCerditRequestModel request, CreditPlanModel creditPlan, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<CreateCerditRequestResponseModel>();

            var userAmount = await GetUserAmountAsync(request.PhoneNumber, request.PlanId, cancellationToken);
            if (userAmount.HasError)
            {
                response.Error = userAmount.Error;
                return response;
            }

            //To Do : change currencyId
            var currencyId = 1;
            var chargeRequest = new ChargeRequestModel(creditPlan.LoanType, currencyId, request.PhoneNumber, (long)creditPlan.GroupId, 10000, Extention.GenerateRandomCode());// userAmount.Data.Amount
            var chargeResponse = await InitializeWalletAsync(chargeRequest, cancellationToken);
            if (chargeResponse.HasError)
            {
                response.Error = chargeResponse.Error;
                return response;
            }

            await InsertCreditRequestAsync(request, CreditRequestStatus.Finalizing, cancellationToken);
            return response;
        }

        #region Private
        private async Task<BaseResponse<ChargeResponseModel>> InitializeWalletAsync(ChargeRequestModel request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<ChargeResponseModel>();

            var createWalletRequest = new CreateWalletRequestModel(request.ConfigType, request.Currency, request.PhoneNumber, request.GroupId);
            var createWallet = await _walletServices.CreateWallet(createWalletRequest, cancellationToken);
            if (createWallet.HasError)
            {
                response.Error = createWallet.Error;
                return response;
            }

            var chargeResponse = await _walletServices.Charge(request, cancellationToken);
            if (chargeResponse.HasError)
            {
                response.Error = chargeResponse.Error;

                var reverseRequest = new ReverseRequestModel(null, request.ClientRefNo);
                var reverseResponse = await _walletServices.Reverse(reverseRequest, cancellationToken);
                if (reverseResponse.HasError)
                    response.Error = reverseResponse.Error;

                return response;
            }
            var adviceRequest = new AdviceRequestModel(chargeResponse.Data.TrackingCode);
            var adviceResponse = await _walletServices.Advice(adviceRequest, cancellationToken);
            if (adviceResponse.HasError)
            {
                response.Error = adviceResponse.Error;
                return response;
            }
            return response;
        }
        private async Task<BaseResponse<LimitationModel>> GetUserAmountAsync(string mobileNumber, long planId, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<LimitationModel>();

            var userInfo = await _userServices.Profile(mobileNumber, cancellationToken);
            if (userInfo.HasError || userInfo.Data is null)
            {
                response.Error = userInfo.HasError ? userInfo.Error : CustomErrors.UserDataIsEmpty;
                return response;
            }

            var personScore = await _inquiryServices.PersonScore(userInfo.Data.NationalCode, cancellationToken);
            if (personScore.HasError || personScore.Data is null)
            {
                response.Error = personScore.HasError ? personScore.Error : CustomErrors.PersonScoreIsEmpty;
                return response;
            }

            var filter = new LimitationFilterModel
            {
                Level = userInfo.Data.Level,
                Score = (int)personScore.Data.Score,
                PlanId = planId
            };

            response.Data = await _limitationRepository.GetAsync(filter, cancellationToken);
            return response;
        }
        private async Task InsertCreditRequestAsync(CreateCerditRequestModel request, CreditRequestStatus requestStep, CancellationToken cancellationToken)
        {
            var model = request.Adapt<CreditRequestModel>();
            model.Status = (byte)requestStep;
            await _creditRequestRepository.AddAsync(model, cancellationToken);
        }
        #endregion
    }
}
