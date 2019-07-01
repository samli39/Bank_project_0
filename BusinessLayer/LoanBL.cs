using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class LoanBL
    {
        CurrentAccount current = new CurrentAccount();
        BankAccountDAL dal = new BankAccountDAL();
        public void CreateLoan(double amount)
        {
            Loan newLoan = new Loan()
            {
                LoanID = new IDgenerator().GenerateID(),
                Amount = Math.Round(amount * 1.1),
                Installment = Math.Round(amount * 1.1 * 0.1)
            };
            current.LoanList.Add(newLoan);
            dal.SaveFile();
        }

        public void PayLoan(int index)
        {
            if (current.LoanList[index].Pay())
            {
                //if the load is all clean,remove the loan from list
                current.LoanList.RemoveAt(index);
                Console.WriteLine("Loan is clean!");
            }


            dal.SaveFile();
            Console.WriteLine("Payment success");
        }
    }
}
