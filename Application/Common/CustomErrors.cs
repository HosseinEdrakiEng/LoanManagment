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

    public static readonly Error CreditPlanNotValid = new("51", "CreditPlan is not valid.", HttpStatusCode.BadRequest);
    public static readonly Error UserDataIsEmpty = new("52", "User data is empty", HttpStatusCode.BadRequest);
    public static readonly Error LoanTypeNotBusiness = new("53", "LoanType is not business", HttpStatusCode.BadRequest);



    public static readonly Error CreateWalletError = new("54", "Create Wallet Is Fail.", HttpStatusCode.BadRequest);
    public static readonly Error ChargeWalletError = new("55", "Charge Wallet Is Fail.", HttpStatusCode.BadRequest);
    public static readonly Error AdviceWalletError = new("56", "Advice Wallet Is Fail.", HttpStatusCode.BadRequest);
    public static readonly Error ReverseWalletError = new("57", "Reverse Wallet Is Fail.", HttpStatusCode.BadRequest);
   
    public static readonly Error UserProfileError = new("58", "User Profile Is Fail.", HttpStatusCode.BadRequest);
    


}

