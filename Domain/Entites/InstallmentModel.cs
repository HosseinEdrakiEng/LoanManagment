using Domain.Entites;
using System;
using System.Collections.Generic;

namespace Domain;

public class InstallmentModel : BaseEntity
{

    public long CreditLoanId { get; set; }

    public DateTime SettlementDate { get; set; }

    public long Amount { get; set; }

    public int Number { get; set; }

    public byte Type { get; set; }

    public bool IsFine { get; set; }

    public long FineAmount { get; set; }

    public byte Status { get; set; }

    public long? InstallmentRefId { get; set; }

    public CreditLoanModel CreditLoan { get; set; } = null!;

    public InstallmentModel? InstallmentRef { get; set; }

    public ICollection<InstallmentModel> InverseInstallmentRef { get; set; } = new List<InstallmentModel>();
}
