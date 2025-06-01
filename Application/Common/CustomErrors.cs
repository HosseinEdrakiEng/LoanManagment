using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common;
public static class CustomErrors
{

    public static readonly Error WalletError = new("51", "Wallet Servis Has Error.", HttpStatusCode.BadRequest);
    public static readonly Error CreditPlanNotValid = new("52", "CreditPlan is not valid.", HttpStatusCode.BadRequest);
    public static readonly Error ApiCallError = new("53", "CallService Has Error.", HttpStatusCode.BadRequest);
    public static readonly Error UserDataIsEmpty = new("54", "User data is empty", HttpStatusCode.BadRequest);
    public static readonly Error LoanTypeNotBusiness = new("55", "LoanType is not business", HttpStatusCode.BadRequest);
}

