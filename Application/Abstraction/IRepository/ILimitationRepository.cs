using Application.Model;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstraction;

public interface ILimitationRepository
{
    Task<LimitationModel> GetAsync(LimitationFilterModel filterModel, CancellationToken cancellationToken);
}
