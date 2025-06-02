using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Model;

public class CreateCerditRequestModel
{
    public long PlanId { get; set; }
    public string UserId { get; set; }
    public string MobileNumber { get; set; }
    public long Amount { get; set; }
}
