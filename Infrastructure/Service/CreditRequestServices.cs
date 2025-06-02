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


