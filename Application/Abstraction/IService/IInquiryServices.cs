using Application.Model;
using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstraction;

public interface IInquiryServices
{
    Task<BaseResponse<RaitingResponseModel>> PersonScore(string nationalCode, CancellationToken cancellationToken);
}
