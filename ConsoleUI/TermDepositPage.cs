using BusinessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    public class TermDepositPage
    {
        private Validation validation = new Validation();
        private TermDepositBL BL = new TermDepositBL();
        private CurrentAccount current = new CurrentAccount();
        public bool TermDepositP()
        {
            int input = 0;
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
                            OpenNewDeposit();
                            break;
                        case 2:
                            CloseDeposit();
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

        private void OpenNewDeposit()
        {
            while (true)
            {
                Console.WriteLine("How much do you want to deposit!\n" +
                                  "PS: the interest will be 10% and maturity is 4 days\n");
                double amount = validation.IsValidAmount(Console.ReadLine());

                if (amount > 0)
                {
                    BL.CreateTermDeposit(amount);
                    break;
                }
            }
        }

        private void CloseDeposit()
        {
            //show the  deposit to close
            int input = ShowDeposit();

            if (input == -1)
                return;
            int checking = ShowChecking();

            BL.CloseDeposit(input, checking);
        }
        private int ShowOption()
        {
            List<string> option = new List<string>();
            option.Add("new Term Deposit");
            option.Add("close Term Deposit");
            option.Add("Back to previous page");
            option.Add("Logout");


            //ask user to choose account
            Console.WriteLine($"Hi {CurrentAccount.Firstname} {CurrentAccount.Lastname} .\n" +
                              "In Term deposit:\n" +
                              $"Please select\n");
            for (int i = 0; i < option.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {option[i]}   ");
            }

            return option.Count;
        }
        private int ShowDeposit()
        {
            int input = 0;
            while (true)
            {
                List<TermDeposit> list = current.TDList;
                Console.WriteLine("Please select:\n");

                //only allow expire term deposit
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Maturity > DateTime.Now)
                        Console.WriteLine($"{list[i].ID} with balance : {list[i].Amount} and maturity is {list[i].Maturity} (not available)\n");
                    else
                        Console.WriteLine($"{i + 1}) {list[i].ID} with balance : {list[i].Amount}\n");
                }

                Console.WriteLine($"{list.Count + 1}) go back\n");
                // check if user enter valid input
                input = validation.IsNum(Console.ReadLine());
                if (input > 0 && input <= list.Count && list[input - 1].Maturity < DateTime.Now)
                    return input - 1;
                else if (input == list.Count + 1)
                    return -1;

            }
        }
        private int ShowChecking()
        {
            int input = 0;
            while (true)
            {
                List<Checking> list = current.CheckingList;
                Console.WriteLine("Please select Checking Account to Deposit:");

                for (int i = 0; i < list.Count; i++)
                {
                    Console.WriteLine($"{i + 1}) {list[i].AccountID} with balance : {list[i].Balance}");
                }


                // check if user enter valid input
                input = validation.IsNum(Console.ReadLine());
                if (input > 0 && input <= list.Count)
                    return input - 1;
            }
        }
    }
}
