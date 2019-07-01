using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public interface IBankAccount
    {
        string AccountID { get; set; }
        double Balance { get; set; }
        string AccountType { get; }
        List<Transaction> TransList { get; set; }

        void Deposit(double amount);
        bool Withdraw(double amount);
    }
}
