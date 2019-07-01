using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Transaction
    {
        public string TransID { get; set;}
        public string TransType { get; set; }
        public string Target { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }

        public override string ToString()
        {
            return $"Transaction {TransID} {TransType} Amount : {Amount:c} {Target} at {Date}";
        }
    }
}
