using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    //a class to save the list of account from json file
    public class AccountList
    {
        private List<Account> acList = new List<Account>();
        public List<Account> AcList
        {
            get => acList; set
            {
                AcList = value;
            }
        }


    }
    public class Account
    {
        public string ID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public override string ToString()
        {
            return $"Firstname is {Firstname} and Lastname is {Lastname} and Username is {Username} and Password is {Password}";
        }
    }
}
