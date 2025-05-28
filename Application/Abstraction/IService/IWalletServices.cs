using Application.Model;
using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstraction.IService;

public interface IWalletServices
{
    Task<BaseResponse<CreateWalletResponseModel>> CreateWallet(CreateWalletRequestModel request, CancellationToken cancellationToken);
    Task<BaseResponse<ChargeResponseModel>> Charge(ChargeRequestModel request, CancellationToken cancellationToken);

    Task<BaseResponse<object>> Advice(AdviceRequestModel request, CancellationToken cancellationToken);
    Task<BaseResponse<object>> Reverse(ReverseRequestModel request, CancellationToken cancellationToken);
}
