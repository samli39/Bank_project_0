using BusinessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    /*
     * Checking Page Console UI
     */
    class CheckingPage
    {
        private Validation validation = new Validation();
        private BankAccountBL BL = new BankAccountBL();
        private CUI ui = new CUI();
        private CurrentAccount current = new CurrentAccount();
        IEnumerable<IBankAccount> option = new CurrentAccount().CheckingList;
        public bool CheckingP()
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
                            BL.OpenChecking();

                            break;
                        case 2:
                            // Close Account
                            CloseCheckingPage();
                            break;
                        case 3:
                            //  Deposit action
                            CheckingDepositPage();
                            
                            break;
                        case 4:
                            //Withdraw action
                            CheckingWithdrawPage();
                            
                            break;
                        case 5:
                            // Transfer action
                            CheckingTransferPage();
                            
                            break;
                        case 6:
                            //  Display all accounts action
                            ShowCheckingList();
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
            }//end of loop

        }

        //closing console page
        public void CloseCheckingPage()
        {
            // loop until user choose go back
            while (true)
            {
                //ask user to select account
                int input = SelectAccount();
                if (input == 0)
                    return;

                //remove the account from the list
                BL.CloseChecking(input - 1);
            }
            
        }
        //deposit console page
        public void CheckingDepositPage()
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

                BL.CheckingDeposit(input-1,amount);
            }
        }

        public void CheckingWithdrawPage()
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
                BL.CheckingWithdraw(input - 1, amount);
            }
        }
        public void CheckingTransferPage()
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
                int type = ui.TransferTo(out to,input,1);
                //ask for the amount to deposit
                double amount = ui.AmountInput("Transfer");

                BL.Transfer(1, input, type, to,amount);

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
                if(--input > current.CheckingList.Count)
                {
                    //the account doesn't have any transaction
                    Console.WriteLine("The account you choose doesn't have any transaction\n");
                }
                else
                {
                    List<Transaction> list = current.CheckingList[input].TransList;
                    Console.WriteLine($"Transaction List of Checking Account : {current.CheckingList[input].AccountID}\n");
                    ui.ShowTransaction(list);
                }
                
            }
                
                
            
        }

        
        //select account form checking 
        private int SelectAccount()
        {
            //loop until valid input
            while (true)
            {
                Console.WriteLine("Please select account :");
                ShowCheckingList();

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
        private void ShowCheckingList()
        {
             ui.ShowList(option);
        }

       



    }
}
