using Application.Model;
using Helper;

namespace Application.Abstraction;

public interface IInquiryServices
{
    Task<BaseResponse<RaitingResponseModel>> PersonScore(string nationalCode, CancellationToken cancellationToken);
}
