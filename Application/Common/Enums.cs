using System.ComponentModel;

namespace Application.Common;

public enum LoanType : byte
{
    [Description("نقدی")]
    Deposit = 1,

    [Description("وام")]
    Loan = 2,

    [Description("اعتبار")]
    Credit = 3,

    [Description("Bnpl")]
    Bnpl = 4,

    [Description("FourInstallment")]
    FourInstallment = 5
}
public enum CreditRequestStatus : byte
{

    [Description("در انتظار آپلود مدارک")]
    WaitUploadDocuments = 1,

    [Description("تکمیل شده")]
    Finalizing = 2,

}
public enum GuarantyType
{
    /// <summary>
    /// ضمانت براساس چک
    /// </summary>
    Cheque = 1,

    /// <summary>
    /// ضمانت براساس سفته
    /// </summary>
    PromissoryNote = 2,

    /// <summary>
    /// گواهی کسر از حقوق
    /// </summary>
    PayrollDeducation = 3,

    /// <summary>
    /// بدون ضمانت
    /// </summary>
    WithoutGuaranty = 4
}
public enum PaymentType
{
    Cheque = 1,
    ChequeOnPurchase = 2,
    LinkPayment = 3,
    DirectDebit = 4
}
