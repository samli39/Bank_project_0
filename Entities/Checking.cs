using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Checking : IBankAccount
    {

        public string AccountID { get; set; }
        public double Balance { get; set; }

        public string AccountType { get; } = "Checking";
        public DateTime Date { get; set; }
        private List<Transaction> transList = new List<Transaction>();
        public List<Transaction> TransList
        {
            get => transList; set
            {
                TransList = value;
            }
        }

        public void Deposit(double amount)
        {
            Balance += amount;
        }

        public bool Withdraw(double amount)
        {
            if (amount > Balance)
            {
                //money in account is less than withdraw amount
                //print the message to
                Console.WriteLine("Cannot Withdraw, Insufficient Funds");
                return false;
            }
            else
            {
                Balance -= amount;
                Console.WriteLine("Successfully withdraw");
                return true;
            }
        }
    }
}
