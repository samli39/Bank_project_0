using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Validation
    {
        //check if user choose the valid select
        public int IsNum(string num)
        {
            int result;
            //try to convert num to int, if success, ans is the result,
            //if not, ans = 0;
            int.TryParse(num, out result);
            return result;
        }

        //check if user enter valid amount
        public double IsValidAmount(string amount)
        {
            double result;
            double.TryParse(amount, out result);

            return result;
        }

        //check if user has entered input
        public Func<string, bool> HasInput = data => data.Length > 0;

        public bool UniqueID(string id)
        {
            CurrentAccount current = new CurrentAccount();
            //check checking list 
            foreach(var ele in current.CheckingList)
            {
                if (ele.AccountID.Equals(id))
                    return false;
            }
            //waiting:business account
            return true;

        }
      
    }
}
