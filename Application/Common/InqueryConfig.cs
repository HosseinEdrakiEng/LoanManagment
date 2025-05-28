using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common;

public class InqueryConfig
{
    public string BaseUrl { get; set; }
    public TimeSpan Timeout { get; set; }
    public string RatingUrl { get; set; }
}
