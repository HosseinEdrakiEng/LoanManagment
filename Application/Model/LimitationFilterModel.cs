using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Model
{
    public class LimitationFilterModel
    {
        public long PlanId { get; set; }
        public int Score { get; set; }
        public string Level { get; set; }
    }
}
