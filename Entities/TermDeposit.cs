using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class TermDeposit
    {
        public string ID { get; set; }
        public double Amount { get; set; }
        public DateTime Maturity { get; set; }
    }
}
