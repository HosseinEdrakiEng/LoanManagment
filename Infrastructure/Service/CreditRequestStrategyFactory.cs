using Application.Abstraction;
using Application.Abstraction;
using Application.Common;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public class CreditRequestStrategyFactory
    {
        private readonly Dictionary<RequestStep, ICreditRequestStrategy> _strategies;

        public CreditRequestStrategyFactory(IEnumerable<ICreditRequestStrategy> strategies)
        {
            _strategies = strategies.ToDictionary(strategy => strategy.GetType().Name.Contains("Upload")
                ? RequestStep.UploadDocumentUploadDocuments
                : RequestStep.Finalizing);
        }

        public ICreditRequestStrategy GetStrategy(CreditPlanModel creditPlan)
        {
            if (creditPlan.GuarantyType.HasValue && creditPlan.GuarantyType > 0)
            {
                return _strategies[RequestStep.UploadDocumentUploadDocuments];
            }

            if (creditPlan.LoanType is (int)LoanType.Bnpl or (int)LoanType.FourInstallment or (int)LoanType.Credit)
            {
                return _strategies[RequestStep.Finalizing];
            }

            throw new InvalidOperationException("Unsupported credit request type.");
        }
    }
}
