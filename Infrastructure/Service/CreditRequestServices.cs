using Application.Abstraction;
using Application.Common;
using Application.Model;
using Azure;
using Azure.Core;
using Domain;
using Helper;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Service;

public class CreditRequestServices : ICreditRequestServices
{
    private readonly ICreditPlanRepository _creditPlanRepository;
    private readonly CreditRequestStrategyFactory _strategyFactory;

    public CreditRequestServices(ICreditPlanRepository creditPlanRepository, CreditRequestStrategyFactory strategyFactory)
    {
        _creditPlanRepository = creditPlanRepository;
        _strategyFactory = strategyFactory;
    }

    public async Task<BaseResponse<CreateCerditRequestResponseModel>> CreateCreditRequest(CreateCerditRequestModel request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<CreateCerditRequestResponseModel>();

        var creditPlan = await _creditPlanRepository.GetAsync(request.PlanId, cancellationToken);
        if (creditPlan == null)
        {
            response.Error = CustomErrors.CreditPlanNotValid;
            return response;
        }

        var strategy = _strategyFactory.GetStrategy(creditPlan);
        return await strategy.ProcessAsync(request, creditPlan, cancellationToken);
    }
}


//public class CreditRequestServices(ICreditRequestRepository creditRequestRepository,
//    ICreditPlanRepository creditPlanRepository, ILimitationRepository limitationRepository,
//    IWalletServices walletServices,IUserServices userServices) : ICreditRequestServices
//{
//    private readonly ICreditRequestRepository creditRequestRepository = creditRequestRepository;
//    private readonly ICreditPlanRepository creditPlanRepository = creditPlanRepository;
//    private readonly IWalletServices walletServices = walletServices;
//    private readonly IUserServices userServices = userServices;
//    private readonly ILimitationRepository limitationRepository = limitationRepository;

//    public async Task<BaseResponse<CreateCerditRequestResponseModel>> CreateCreditRequest(
//    CreateCerditRequestModel request, CancellationToken cancellationToken)
//    {
//        var response = new BaseResponse<CreateCerditRequestResponseModel>();

//        var creditPlan = await creditPlanRepository.GetAsync(request.PlanId, cancellationToken);
//        if (creditPlan == null)
//        {
//            response.Error = CustomErrors.CreditPlanNotValid;
//            return response;
//        }

//        //await InsertCerditRequest(request, RequestStep.Create, cancellationToken);

//        if (creditPlan.GuarantyType.HasValue && creditPlan.GuarantyType > 0)
//        {
//            await InsertCerditRequest(request, RequestStep.UploadDocumentUploadDocuments, cancellationToken);
//            return response;
//        }

//        if (creditPlan.LoanType is (int)LoanType.Bnpl or (int)LoanType.FourInstallment or (int)LoanType.Credit)
//        {

//            var userAmount = await GetUserAmount(request.MobileNumber, request.PlanId, cancellationToken);
//            if (userAmount.HasError)
//            {
//                response.Error = userAmount.Error;
//                return response;
//            }

//            //To Do : change currencyId
//            var currencyId = 1;
//            var chargeRequest = new ChargeRequestModel(creditPlan.LoanType, currencyId, request.MobileNumber, (long)creditPlan.GroupId, userAmount.Data.Amount, Extention.GenerateRandomCode());
//            var chargeResponse = await InitializeWalletAsync(chargeRequest, cancellationToken);
//            if (chargeResponse.HasError)
//            {
//                response.Error = chargeResponse.Error;
//                return response;
//            }

//            await InsertCerditRequest(request, RequestStep.Finalizing, cancellationToken);
//        }
//        else
//            response.Error = CustomErrors.LoanTypeNotBusiness;

//        return response;
//    }

//    #region private
//    private async Task InsertCerditRequest(CreateCerditRequestModel request, RequestStep requestStep, CancellationToken cancellationToken)
//    {
//        var model = request.Adapt<CreditRequestModel>();
//        model.Status = (byte)requestStep;
//        await creditRequestRepository.AddAsync(model, cancellationToken);
//    }

//    private async Task<BaseResponse<LimitationModel>> GetUserAmount(string mobileNumber, long planId, CancellationToken cancellationToken)
//    {
//        var response = new BaseResponse<LimitationModel>();
//        var userInfo = await userServices.Profile(mobileNumber, cancellationToken);
//        if (userInfo.HasError || userInfo.Data is null)
//        {
//            response.Error = userInfo.HasError? userInfo.Error:CustomErrors.UserDataIsEmpty;
//            return response;
//        }
//        var filter = new LimitationFilterModel()
//        {
//            Level = userInfo.Data?.Level,
//            Score = int.Parse(userInfo.Data?.Score),
//            PlanId = planId
//        };
//        response.Data = await limitationRepository.GetAsync(filter, cancellationToken);
//        return response;

//    }

//    private async Task<BaseResponse<ChargeResponseModel>> InitializeWalletAsync(ChargeRequestModel request, CancellationToken cancellationToken)
//    {
//        var response = new BaseResponse<ChargeResponseModel>();

//        var createWalletRequest = new CreateWalletRequestModel(request.ConfigType, request.Currency, request.PhoneNumber, request.GroupId);
//        var createWallet = await walletServices.CreateWallet(createWalletRequest, cancellationToken);
//        if (createWallet.HasError)
//        {
//            response.Error = createWallet.Error;
//            return response;
//        }

//        var chargeResponse = await walletServices.Charge(request, cancellationToken);
//        if (chargeResponse.HasError)
//        {
//            response.Error = chargeResponse.Error;

//            var reverseRequest = new ReverseRequestModel(null,request.ClientRefNo);
//            var reverseResponse = await walletServices.Reverse(reverseRequest, cancellationToken);
//            if (reverseResponse.HasError)
//                response.Error = reverseResponse.Error;

//            return response;
//        }
//        var adviceRequest = new AdviceRequestModel(chargeResponse.Data.TrackingCode);
//        var adviceResponse =  await walletServices.Advice(adviceRequest, cancellationToken);
//        if (adviceResponse.HasError)
//        {
//            response.Error = adviceResponse.Error;
//            return response;
//        }
//        return response;
//    }

//    #endregion

//}