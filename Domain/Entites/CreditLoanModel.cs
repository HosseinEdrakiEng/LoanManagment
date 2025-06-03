using Domain.Entites;

namespace Domain;

public class CreditLoanModel : BaseEntity
{

    public long CreditRequestId { get; set; }

    public long InstallmentSum { get; set; }

    public long CommissionAmount { get; set; }

    public long AnnualInterestAmount { get; set; }

    public byte Status { get; set; }

    public DateTime EndDate { get; set; }

    public long Amount { get; set; }


    public CreditRequestModel CreditRequest { get; set; } = null!;

    public ICollection<InstallmentModel> Installments { get; set; } = new List<InstallmentModel>();
}
