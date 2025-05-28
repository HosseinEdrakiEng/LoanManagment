using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Model;

public class CreditPlanFilter
{
    public string? Title { get; set; }

    public bool? Enable { get; set; }

    public byte? ConfigTypeId { get; set; }

    public long? CurrencyId { get; set; }
}
