using Domain.Entites;
using System;
using System.Collections.Generic;

namespace Domain;

public class CreditPlanModel : BaseEntity
{

    public string Title { get; set; } = null!;

    public int UsagePeriod { get; set; }

    public int? RepaymentDeadline { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public int FineRate { get; set; }

    public int? GuarantyType { get; set; }

    public int LoanType { get; set; }

    public long? GroupId { get; set; }

    public int? InstalmentType { get; set; }

    public int? PaymentType { get; set; }

    public int AnnualInterestRate { get; set; }

    public int CommissionPercentage { get; set; }

    public int? InstallmentCount { get; set; }

    public int? PrePaymentPercentage { get; set; }

    public int? CommissionCalculateType { get; set; }

    public virtual ICollection<CreditRequestModel> CreditRequests { get; set; } = new List<CreditRequestModel>();

    public virtual ICollection<LimitationModel> Limitations { get; set; } = new List<LimitationModel>();
}
