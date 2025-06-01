using Application.Abstraction;
using Application.Abstraction.IService;
using Application.Common;
using Application.Model;
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

public class CreditRequestServices(ICreditRequestRepository creditRequestRepository,
    ICreditPlanRepository creditPlanRepository, ILimitationRepository limitationRepository,
    IWalletServices walletServices,IUserServices userServices) : ICreditRequestServices
{
    private readonly ICreditRequestRepository creditRequestRepository = creditRequestRepository;
    private readonly ICreditPlanRepository creditPlanRepository = creditPlanRepository;
    private readonly IWalletServices walletServices = walletServices;
    private readonly IUserServices userServices = userServices;
    private readonly ILimitationRepository limitationRepository = limitationRepository;

    public async Task<BaseResponse<CreateCerditRequestResponseModel>> CreateCreditRequest(
    CreateCerditRequestModel request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<CreateCerditRequestResponseModel>();

        await InsertCerditRequest(request, RequestStep.Create, cancellationToken);

        var creditPlan = await creditPlanRepository.GetAsync(request.PlanId, cancellationToken);
        if (creditPlan == null)
        {
            response.Error = CustomErrors.CreditPlanNotValid;
            return response;
        }

        if (creditPlan.GuarantyType.HasValue && creditPlan.GuarantyType > 0)
        {
            await InsertCerditRequest(request, RequestStep.UploadDocumentUploadDocuments, cancellationToken);
            return response;
        }

        if (creditPlan.LoanType is (int)LoanType.Bnpl or (int)LoanType.FourInstallment or (int)LoanType.Credit)
        {

            var userAmount = await GetUserAmount(request.MobileNumber, request.PlanId, cancellationToken);
            if (userAmount.HasError)
            {
                response.Error = userAmount.Error;
                return response;
            }

            //To Do : change currencyId
            var createWalletRequest = new CreateWalletRequestModel(creditPlan.LoanType, 1, request.MobileNumber, (long)creditPlan.GroupId);
            var createWallet = await walletServices.CreateWallet(createWalletRequest, cancellationToken);
            if (createWallet.HasError)
            {
                response.Error = createWallet.Error;
                return response;
            }

            string clientRefNo = Extention.GenerateRandomCode();
            var chargeRequest = new ChargeRequestModel(creditPlan.LoanType, 1, request.MobileNumber, (long)creditPlan.GroupId, userAmount.Data.Amount, clientRefNo);
            var chargeResponse = await walletServices.Charge(chargeRequest, cancellationToken);
            if (chargeResponse.HasError)
            {
                response.Error = chargeResponse.Error;
                return response;
            }

            await InsertCerditRequest(request, RequestStep.Finalizing, cancellationToken);
        }
        else
        {
            response.Error = CustomErrors.LoanTypeNotBusiness;
        }

        return response;
    }

    #region private
    private async Task InsertCerditRequest(CreateCerditRequestModel request, RequestStep requestStep, CancellationToken cancellationToken)
    {
        var model = request.Adapt<CreditRequestModel>();
        model.Status = (byte)requestStep;
        await creditRequestRepository.AddAsync(model, cancellationToken);
    }

    private async Task<BaseResponse<LimitationModel>> GetUserAmount(string mobileNumber, long planId, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<LimitationModel>();
        var userInfo = await userServices.Profile(mobileNumber, cancellationToken);
        if (userInfo.HasError || userInfo.Data is null)
        {
            response.Error = userInfo.HasError? userInfo.Error:CustomErrors.UserDataIsEmpty;
            return response;
        }
        var filter = new LimitationFilterModel()
        {
            Level = userInfo.Data?.Level,
            Score = int.Parse(userInfo.Data?.Score),
            PlanId = planId
        };
        response.Data = await limitationRepository.GetAsync(filter, cancellationToken);
        return response;

    }

    #endregion

}