using Application.Abstraction;
using Application.Common;
using Application.Model;
using Domain;
using Helper;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service;

public class UploadDocumentStrategy : ICreditRequestStrategy
{
    private readonly ICreditRequestRepository _creditRequestRepository;

    public UploadDocumentStrategy(ICreditRequestRepository creditRequestRepository)
    {
        _creditRequestRepository = creditRequestRepository;
    }

    public async Task<BaseResponse<CreateCerditRequestResponseModel>> ProcessAsync(CreateCerditRequestModel request, CreditPlanModel creditPlan, CancellationToken cancellationToken)
    {
        await InsertCreditRequestAsync(request, RequestStep.UploadDocumentUploadDocuments, cancellationToken);
        return new BaseResponse<CreateCerditRequestResponseModel>();
    }

    private async Task InsertCreditRequestAsync(CreateCerditRequestModel request, RequestStep requestStep, CancellationToken cancellationToken)
    {
        var model = request.Adapt<CreditRequestModel>();
        model.Status = (byte)requestStep;
        await _creditRequestRepository.AddAsync(model, cancellationToken);
    }
}
