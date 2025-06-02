using Application.Model;
using Helper;

namespace Application.Abstraction;

public interface IUserServices
{
    Task<BaseResponse<UserProfileResponseModel>> Profile(string phoneNumber, CancellationToken cancellationToken);
}
