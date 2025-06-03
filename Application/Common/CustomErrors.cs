using Helper;
using System.Net;

namespace Application.Common;
public static class CustomErrors
{

    public static readonly Error CreditPlanNotValid = new("51", "CreditPlan is not valid.", HttpStatusCode.BadRequest);
    public static readonly Error UserDataIsEmpty = new("52", "User data is empty", HttpStatusCode.BadRequest);
    public static readonly Error LoanTypeNotBusiness = new("53", "LoanType is not business", HttpStatusCode.BadRequest);

    public static readonly Error CreateWalletError = new("54", "Create Wallet is fail.", HttpStatusCode.BadRequest);
    public static readonly Error ChargeWalletError = new("55", "Charge Wallet is fail.", HttpStatusCode.BadRequest);
    public static readonly Error AdviceWalletError = new("56", "Advice Wallet is fail.", HttpStatusCode.BadRequest);
    public static readonly Error ReverseWalletError = new("57", "Reverse Wallet is fail.", HttpStatusCode.BadRequest);
   
    public static readonly Error UserProfileError = new("58", "User Profile is fail.", HttpStatusCode.BadRequest);
    public static readonly Error PersonScoreError = new("59", "Person Score is fail.", HttpStatusCode.BadRequest);
    public static readonly Error PersonScoreIsEmpty = new("60", "Person Score is empty", HttpStatusCode.BadRequest);


    public static readonly Error ApiCallError = new("61", "CallService Has Error.", HttpStatusCode.BadRequest);
    public static readonly Error UndefinedPlan = new("62", "Undefined plan", HttpStatusCode.BadRequest);
    public static readonly Error WalletError = new("63", "Wallet Servis Has Error.", HttpStatusCode.BadRequest);
    public static readonly Error LimitationNotFound = new("64", "Limitation not found", HttpStatusCode.BadRequest);
}

