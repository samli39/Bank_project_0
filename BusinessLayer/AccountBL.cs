using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class AccountBL
    {
        AccountDAL dal = new AccountDAL();

        //for Register Page
        public bool Register(Dictionary<string, string> info)
        {

            Account ac = new Account()
            {
                Firstname = info["First Name"],
                Lastname = info["Last Name"],
                Username = info["Username"],
                Password = info["Password"]
            };

            //pass to DAL to save to database
            if (!dal.AccountRegister(ac))
            {
                return false;
            }
            return true;
        }

        //check if user has registered.
        public  bool AccountMatch(string username, string password)
        { 
            return dal.AccountMatch(username, password);
        }
    }
}

