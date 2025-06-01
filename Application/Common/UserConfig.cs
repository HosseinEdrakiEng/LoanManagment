using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common;

public class UserConfig
{
    public string BaseUrl { get; set; }
    public TimeSpan Timeout { get; set; }
    public string ProfileUrl { get; set; }

}