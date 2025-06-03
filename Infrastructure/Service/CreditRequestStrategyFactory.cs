using Application.Abstraction;
using Application.Common;
using Domain;

namespace Infrastructure.Service
{
    public class CreditRequestStrategyFactory
    {
        private readonly Dictionary<CreditRequestStatus, ICreditRequestStrategy> _strategies;

        public CreditRequestStrategyFactory(IEnumerable<ICreditRequestStrategy> strategies)
        {
            _strategies = strategies.ToDictionary(strategy => strategy.GetType().Name.Contains("Upload")
                ? CreditRequestStatus.WaitUploadDocuments
                : CreditRequestStatus.Finalizing);
        }

        public ICreditRequestStrategy GetStrategy(CreditPlanModel creditPlan)
        {
            if (creditPlan.GuarantyType.HasValue && creditPlan.GuarantyType > 0)
            {
                return _strategies[CreditRequestStatus.WaitUploadDocuments];
            }

            if (creditPlan.LoanType is (int)LoanType.Bnpl or (int)LoanType.FourInstallment or (int)LoanType.Credit)
            {
                return _strategies[CreditRequestStatus.Finalizing];
            }

            throw new InvalidOperationException("Unsupported credit request type.");
        }
    }
}
