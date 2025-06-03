using Application.Abstraction;
using Application.Common;
using Application.Model;
using Domain;
using Helper;
using Mapster;

namespace Infrastructure.Service;

public class CreditRequestServices : ICreditRequestServices
{
    private readonly ICreditPlanRepository _creditPlanRepository;
    private readonly ILimitationRepository _limitationRepository;
    private readonly ICreditRequestRepository _creditRequestRepository;
    private readonly IInquiryServices _inquiryServices;
    private readonly IUserServices _userServices;
    private readonly IWalletServices _walletServices;

    public CreditRequestServices(ICreditPlanRepository creditPlanRepository
        , IInquiryServices inquiryServices
        , ILimitationRepository limitationRepository
        , ICreditRequestRepository creditRequestRepository
        , IWalletServices walletServices
        , IUserServices userServices)
    {
        _creditPlanRepository = creditPlanRepository;
        _inquiryServices = inquiryServices;
        _limitationRepository = limitationRepository;
        _creditRequestRepository = creditRequestRepository;
        _walletServices = walletServices;
        _userServices = userServices;
    }

    public async Task<BaseResponse<CreateCerditRequestResponseModel>> CreateCreditRequest(CreateCerditRequestModel request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<CreateCerditRequestResponseModel>();

        var creditPlan = await _creditPlanRepository.GetAsync(request.PlanId, cancellationToken);
        if (creditPlan is null)
        {
            response.Error = CustomErrors.CreditPlanNotValid;
            return response;
        }

        var model = request.Adapt<CreditRequestModel>();
        if (creditPlan.GuarantyType.HasValue && creditPlan.GuarantyType > 0)
        {
            model.Status = (byte)CreditRequestStatus.WaitUploadDocuments;
        }
        else
        {
            var personScore = await _inquiryServices.PersonScore(request.NationalCode, cancellationToken);
            if (personScore.HasError)
            {
                response.Error = personScore.Error;
                return response;
            }

            var user = await _userServices.Profile(request.PhoneNumber, cancellationToken);
            if (user.HasError)
            {
                response.Error = user.Error;
                return response;
            }

            var limitationModel = new LimitationFilterModel
            {
                Level = user.Data.Level,
                Score = (int)personScore.Data.Score,
                PlanId = request.PlanId
            };

            var limit = await _limitationRepository.GetAsync(limitationModel, cancellationToken);
            if (limit is null)
            {
                response.Error = CustomErrors.LimitationNotFound;
                return response;
            }

            var chargeRequest = new ChargeRequestModel(creditPlan.LoanType, creditPlan.CurrencyId, request.PhoneNumber, (long)creditPlan.GroupId, limit.Amount, Extention.GenerateRandomCode());
            var chargeResponse = await InitializeWalletAsync(chargeRequest, cancellationToken);
            if (chargeResponse.HasError)
            {
                response.Error = chargeResponse.Error;
                return response;
            }

            model.Status = (byte)CreditRequestStatus.Finalizing;
            model.Limitation = limit;
            model.Amount = limit.Amount;
            model.CreditPlan = creditPlan;
            await _creditRequestRepository.AddAsync(model, cancellationToken);
        }

        return response;
    }

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
}


