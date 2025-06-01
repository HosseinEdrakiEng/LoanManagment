using Domain.Entites;
using System;
using System.Collections.Generic;

namespace Domain;

public  class CreditLoanModel : BaseEntity
{

    public long CreditRequestId { get; set; }

    public long InstallmentSum { get; set; }

    public long CommissionAmount { get; set; }

    public long AnnualInterestAmount { get; set; }

    public byte Status { get; set; }

    public DateTime EndDate { get; set; }

    public long Amount { get; set; }


    public virtual CreditRequestModel CreditRequest { get; set; } = null!;

    public virtual ICollection<InstallmentModel> Installments { get; set; } = new List<InstallmentModel>();
}
