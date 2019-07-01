using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Business:IBankAccount
    {
        public string AccountID { get; set; }
        public double Balance { get; set; }
        public bool Overdraft = false;
        //overdraft amount
        private int oAmount = 100;
        public string AccountType { get; } = "Business";
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
            //after deposit, it balance is no negative, then no overdraft
            if (Balance >= 0)
                Overdraft = false;

        }

        public bool Withdraw(double amount)
        {
            //check if the withdraw amount is greater than balance plus overdraft
            //also, check if it already overdraft
            if (Overdraft || amount > (Balance + oAmount))
            {
                //funds in account is less than withdraw amount
                //print the message to
                Console.WriteLine("Cannot Withdraw, Insufficient Funds");
                return false;
            }

            else
            {
                Console.WriteLine("Successfully withdraw");
                Balance -= amount;
                //after withdrawn, if balance less than 0, then it has overdraft
                if (Balance < 0)
                {
                    Overdraft = true;
                    //user overdraft, we charge for that by 10% of overdraft
                    Balance *= 1.1;
                }


                return true;
            }

        }
    }
}
