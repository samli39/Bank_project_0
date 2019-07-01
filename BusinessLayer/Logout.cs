using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Logout
    {
        //clear the all data in CurrentAccount for next sign in
        public Logout()
        {
            CurrentAccount current = new CurrentAccount();
            //before logout, remove all data in CurrentAcoount
            CurrentAccount.Firstname = null;
            CurrentAccount.Lastname = null;
            CurrentAccount.Username = null;
            
            current.BusinessList.Clear();
            current.CheckingList.Clear();
            current.LoanList.Clear();
        }
    }
}
