using Application.Model;
using Domain;
using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstraction;

public interface ICreditRequestStrategy
{
    Task<BaseResponse<CreateCerditRequestResponseModel>> ProcessAsync(CreateCerditRequestModel request, CreditPlanModel creditPlan, CancellationToken cancellationToken);
}


