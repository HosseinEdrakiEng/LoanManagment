namespace Application.Model
{
    public class LoadCreditPlanRequestModel
    {
        public string ClientId { get; set; }
        public string UserId { get; set; }
        public long GroupId { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalCode { get; set; }
    }
    public class LoadCreditPlanResponseModel
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

        public long Amount { get; set; }

        public long Id { get; set; }
    }
}
