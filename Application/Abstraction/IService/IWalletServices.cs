using Application.Model;
using Helper;

namespace Application.Abstraction;

public interface IWalletServices
{
    Task<BaseResponse<CreateWalletResponseModel>> CreateWallet(CreateWalletRequestModel request, CancellationToken cancellationToken);
    Task<BaseResponse<ChargeResponseModel>> Charge(ChargeRequestModel request, CancellationToken cancellationToken);
    Task<BaseResponse<AdviceResponseModel>> Advice(AdviceRequestModel request, CancellationToken cancellationToken);
    Task<BaseResponse<ReverseResponseModel>> Reverse(ReverseRequestModel request, CancellationToken cancellationToken);
}
