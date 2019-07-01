using BusinessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    public class LoanPage
    {
        private Validation validation = new Validation();
        private LoanBL BL = new LoanBL();
        private CurrentAccount current = new CurrentAccount();
        public bool LoanP()
        {

            int input = 0;
            //loop until valid input or choose go back or logout
            while (true)
            {
                int count = ShowOption();
                // check if user enter valid input
                input = validation.IsNum(Console.ReadLine());
                //validate the input
                if (input > 0 && input <= count)
                    switch (input)
                    {
                        case 1:
                            // create New Loan
                            NewLoan();
                            break;
                        case 2:

                            //pay installment
                            PayLoan();

                            break;
                        case 3:
                            //back to previous page
                            return false;
                        case 4:
                            //clear all data in CurrentAccount
                            new Logout();
                            // back to Home page
                            return true;
                    }



            }// end of while loop
        }

        private void NewLoan()
        {
            while (true)
            {
                Console.WriteLine("How much do you need!\n" +
                                  "PS: the interest will be 10% for the Loan(round up if it is decmial number).\n");
                double amount = validation.IsValidAmount(Console.ReadLine());

                if (amount > 0)
                {
                    BL.CreateLoan(amount);
                    break;
                }
            }
        }

        public void PayLoan()
        {

            //Show the list of Loan
            int input = ShowLoanList();
            if (input == current.LoanList.Count+1)
                return;
            BL.PayLoan(input-1);
        }
        private int ShowOption()
        {
            List<string>option = new List<string>();
            option.Add("New Loan");
            option.Add("Pay Installment");
            option.Add("Back to previous page");
            option.Add("Logout");


            //ask user to choose account
            Console.WriteLine($"Hi {CurrentAccount.Firstname} {CurrentAccount.Lastname} .\n" +
                              "In Loan:\n"+
                              $"Please select\n");
            for (int i = 0; i < option.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {option[i]}   ");
            }

            return option.Count;
        }
        private int ShowLoanList()
        {
            while (true)
            {
                List<Loan> loanList = current.LoanList;
                Console.WriteLine("Please select a Loan");
                for (int i = 0; i < loanList.Count; i++)
                {
                    Console.WriteLine($"{i + 1}) ID {loanList[i].LoanID} with Loan : {loanList[i].Amount} and installment : {loanList[i].Installment}");
                }
                Console.WriteLine($"{loanList.Count+1}) go back");
                // check if user enter valid input
                int input = validation.IsNum(Console.ReadLine());
                if (input > 0 && input <= loanList.Count+1)
                    return input;
            }


        }
    }
}
