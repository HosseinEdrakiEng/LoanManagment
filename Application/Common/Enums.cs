using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common;

public enum NotRegisterationStatusType : byte
{
    None = 0,
    Done = 1
}
public enum CreditPlanType : byte
{
    None = 0
}
public enum ConfigType : byte
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
