using BusinessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    public class BusinessPage
    {
        private Validation validation = new Validation();
        private BankAccountBL BL = new BankAccountBL();
        private CUI ui = new CUI();
        private CurrentAccount current = new CurrentAccount();
        IEnumerable<IBankAccount> option = new CurrentAccount().BusinessList;

        public bool BusinessP()
        {
            int input = 0;

            //loop until valid input or choose go back or logout
            while (true)
            {
                int count = ui.ShowAccountOption();
                // check if user enter valid input
                input = validation.IsNum(Console.ReadLine());

                //validate the input
                if (input > 0 && input <= count)
                    switch (input)
                    {
                        case 1:
                            //open new account
                            BL.OpenBusiness();

                            break;
                        case 2:
                            // Close Account
                            CloseBusinessPage();
                            break;
                        case 3:
                            //  Deposit action
                            BusinessDepositPage();

                            break;
                        case 4:
                            //Withdraw action
                            BusinessWithdrawPage();

                            break;
                        case 5:
                            // Transfer action
                            BusinessTransferPage();
                            
                            break;
                        case 6:
                            //  Display all accounts action
                            ShowBusinessList();
                            Console.WriteLine("Click any key to go back!");
                            Console.ReadLine();
                            break;
                        case 7:
                            // show list of transaction
                            ShowAllTransactionPage();
                            break;
                        case 8:
                            //back to previous page
                            return false;
                        case 9:
                            //clear all data in CurrentAccount
                            new Logout();
                            // back to Home page
                            return true;
                    }
            }
        }


        public void CloseBusinessPage()
        {
            // loop until user choose go back
            while (true)
            {
                //ask user to select account
                int input = SelectAccount();
                if (input == 0)
                    return;

                //remove the account from the list
                BL.CloseBusiness(input - 1);
            }
        }
        public void BusinessDepositPage()
        {
            // loop until user choose go back
            while (true)
            {
                //ask user to select account
                int input = SelectAccount();
                if (input == 0)
                    return;

                //ask for the amount to deposit
                double amount = ui.AmountInput("Deposit");

                BL.BusinessDeposit(input - 1, amount);
            }
        }

        public void BusinessWithdrawPage()
        {
            //loop until user choose go back
            while (true)
            {
                //ask user to select account
                int input = SelectAccount();
                if (input == 0)
                    return;

                //ask for the amount to deposit
                double amount = ui.AmountInput("Withdraw");
                BL.BusinessWithdraw(input - 1, amount);
            }
        }


        private void BusinessTransferPage()
        {
            //loop until user choose go back
            while (true)
            {
                //ask user to select account transfer from
                int input = SelectAccount();
                if (input-- == 0)
                    return;

                //ask for the account to transfer to
                int to;
                int type = ui.TransferTo(out to, input, 2);
                //ask for the amount to deposit
                double amount = ui.AmountInput("Transfer");

                BL.Transfer(2, input, type, to, amount);

            }
        }
        public void ShowAllTransactionPage()
        {
            //loop until go back
            while (true)
            {
                //ask user to select account
                int input = SelectAccount();
                if (input == 0)
                    return;

                //show the transaction of an account
                if (--input > current.BusinessList.Count)
                {
                    //the account doesn't have any transaction
                    Console.WriteLine("The account you choose doesn't have any transaction\n");
                }
                else
                {
                    List<Transaction> list = current.BusinessList[input].TransList;
                    Console.WriteLine($"Transaction List of Business Account : {current.BusinessList[input].AccountID}\n");
                    ui.ShowTransaction(list);
                }

            }
        }
        public int SelectAccount()
        {
            //loop until valid input
            while (true)
            {
                Console.WriteLine("Please select account :");
                ShowBusinessList();

                int input = validation.IsNum(Console.ReadLine());
                //validate the input
                if (input > 0 && input <= option.Count() + 1)
                {
                    if (input == option.Count() + 1)
                        //go back
                        return 0;
                    else
                    {
                        return input;
                    }
                }

            }
        }

        //show list of checking account
        private void ShowBusinessList()
        {
            ui.ShowList(option);
        }
    }
}
