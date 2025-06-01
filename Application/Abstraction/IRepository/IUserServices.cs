using Application.Model;
using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstraction;

public interface IUserServices
{
    Task<BaseResponse<UserProfileResponseModel>> Profile(string mobileNumber, CancellationToken cancellationToken);
}
