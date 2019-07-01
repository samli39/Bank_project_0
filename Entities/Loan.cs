using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Loan
    {
        public string LoanID { get; set; }
        public double Amount { get; set; }
        public double Installment { get; set; }


        public bool Pay()
        {

            Amount -= Installment;
            //check if the loan is clean
            if (Amount <= 0)
                return true;
            else
                return false;
        }
    }
}
