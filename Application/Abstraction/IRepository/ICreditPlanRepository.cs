using Application.Model;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstraction.IRepository;

public interface ICreditPlanRepository
{
    Task<CreditPlan> GetByFilter(CreditPlanFilter filter, CancellationToken cancellationToken);
}
