using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common;


public enum CreditPlanType : byte
{
    None = 0
}
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
public enum RequestStep : byte
{

    [Description("در انتظار آپلود مدارک")]
    UploadDocumentUploadDocuments = 1,

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

    /// <summary>
    /// پرداخت چک به ازای اقساط
    /// </summary>
    [Description("پرداخت چک به ازای اقساط")]
    ByCheque = 1,

    /// <summary>
    /// پرداخت نقدی اقساط در موعد مقرر
    /// </summary>
    [Description("پرداخت نقدی اقساط در موعد مقرر")]
    ByInstallment = 2

}
