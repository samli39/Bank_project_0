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
         The console UI is in this file
         1.Home Menu Page
         2.Register Page
         3.SignIn Page
         4.MainMenu Page
    */
    public class CUI
    {
        private Validation validation = new Validation();
        private AccountBL BL = new AccountBL();
        private List<String> option;
        //Main Menu Page
        public int HomePage()
        {
            int input = 0;
            //loop unit user enter a correct number
            while (true)
            {
                //ask user to sign in or register a new account(not bank account)
                Console.WriteLine("Please Enter a valid number to select service \n" +
                                  "1) Sign in  2) Register ");

                //check if user enter valid input
                input = validation.IsNum(Console.ReadLine());

                //validate the input
                if (input > 0 && input <= 2)
                    break;
            }

            return input;
        }

        //Register Page
        public void RegisterPage()
        {
            //loop until data sucessfully save to database or user choose go back 
            while (true)
            {
                //create a Dictionary to save information
                Dictionary<string, string> form = new Dictionary<string, string>();
                form.Add("First Name", "");
                form.Add("Last Name", "");
                form.Add("Username", "");
                form.Add("Password", "");

                //use loop to ask question
                //the ToArray() allow us to modify the value in Dictionary
                foreach (KeyValuePair<string, string> ele in form.ToArray())
                {
                    //loop until user enter valid data
                    while (true)
                    {
                        Console.WriteLine("Please enter your " + ele.Key + " or enter \"back\" to back to main menu");
                        string result = Console.ReadLine();

                        if (result.ToLower().Equals("back"))
                            //if user enter "quit",back to main menu
                            return;
                        else
                        {
                            //check if user has entered data
                            if (validation.HasInput(result))
                            {
                                //save it to the list
                                form[ele.Key] = result;
                                //if yes, break the inner loop
                                break;
                            }
                        }// end of else statment
                    }// end of inner loop
                }

                    //all data has been filled in 
                    //save it to database
                    if (BL.Register(form))
                    {
                        Console.WriteLine("successfully create a account\n");
                        break;
                    }
                
            }//end of outter loop
        }

        //Sign in Page
        public bool SignInPage()
        {
            //loop until valid input or user choose 'back'
            while (true)
            {
                // ask for username 
                Console.WriteLine("Please enter your Username:");
                string username = Console.ReadLine();

                //passowrd
                Console.WriteLine("Please enter your password:  or enter \"Back\" to main menu");
                string password = Console.ReadLine();

                if (password.ToLower().Equals("back"))
                    return false;

                //check if the account exists
                //if exist, the AccountMatch method would also save information of account to CurrentAccount Class
                if (!BL.AccountMatch(username, password))
                {
                    Console.WriteLine("Username or Passward doesn't  match. Please try again.\n");
                }
                else
                {
                    //after sign in,open or create json file for saving bank account information
                    if (new BankAccountBL().FetchInfo())
                        break;
                }
            }//end of loop

            return true;
        }

        //Main Menu Page
        public void MainMenuPage()
        {
            int input = 0;
            bool logout = false;

            //loop until valid input or logout
            while (true)
            {
                showMainOption();
                // check if user enter valid input
                input = validation.IsNum(Console.ReadLine());

                //validate the input
                if (input > 0 && input <= 5)
                    switch (input)
                    {
                        case 1:
                            // CheckingPage
                            logout = new CheckingPage().CheckingP();
                            
                            if (logout)
                                return;
                            break;
                        case 2:
                            // businessPage
                            logout = new BusinessPage().BusinessP();
                            
                            if (logout)
                                return;
                            break;
                        case 3:
                            // Loan Page
                            logout = new LoanPage().LoanP();
                            
                            if (logout)
                                return;
                            break;
                        case 4:
                            //  Term Deposit Page
                            logout = new TermDepositPage().TermDepositP();
                            
                            if (logout)
                                return;
                            break;
                        case 5:
                            //clear all data in CurrentAccount
                            new Logout();
                            // back to Home page
                            return;
                    }

            }//end of loop
        }

        

        //show type of account
        public void showMainOption()
        {
            option = new List<string>();
            option.Add("Checking Account");
            option.Add("Business Account");
            option.Add("Loan");
            option.Add("Term Deposit");
            option.Add("Logout");

            //ask user to choose account
            Console.WriteLine($"Hi {CurrentAccount.Firstname} {CurrentAccount.Lastname} .\n" +
                             $"How can I help you.\n" +
                             $"Please select\n");
            for(int i = 0; i < option.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {option[i]}   ");
            }
        }

        /*
         *  both checking and business account related method
         */
        //show type of service
        public int ShowAccountOption()
        {
            option = new List<string>();
            option.Add("Open Account");
            option.Add("Close Account");
            option.Add("Deposit");
            option.Add("Withdraw");
            option.Add("Transfer");
            option.Add("Display All Accounts");
            option.Add("Display Transaction");
            option.Add("Back to previous page");
            option.Add("Logout");

            //ask user to choose account
            Console.WriteLine($"Hi {CurrentAccount.Firstname} {CurrentAccount.Lastname} .\n" +
                             $"Please select\n");
            for (int i = 0; i < option.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {option[i]}   ");
            }

            return option.Count;
        }
        //show account list of checking or business
        public void ShowList(IEnumerable<IBankAccount> list)
        {
            int index = 1;
            foreach(var ele in list)
            {
                Console.WriteLine($"{index++}) {ele.AccountID} and balance is {ele.Balance:c}");
            }

            Console.WriteLine($"{index}) Go back\n");

        }

        //ask user for amount of deposit and withdraw , transfer
        public double AmountInput(string action)
        {
            //loop until valid input
            while (true)
            {
                Console.WriteLine($"Please enter a amount to {action}!!");
                //validate the input
                double amount = validation.IsValidAmount(Console.ReadLine());

                if (amount > 0)
                    return amount;
            }
            
        }

        public void ShowTransaction(List<Transaction> list)
        {
            
            for(int i = 0; i < list.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {list[i].ToString()}");
            }

            Console.WriteLine("Click any key to go back");
            Console.ReadLine();

        }

        /*
         * tranfer method for checking and business
         */
         //type 1 = checking, 2 = business
         public int TransferTo(out int to,int from ,int type)
        {
            CurrentAccount currentAccount = new CurrentAccount();
            int checkingCount = currentAccount.CheckingList.Count;
            int businessCount = currentAccount.BusinessList.Count + checkingCount;
            List<Checking> cList = currentAccount.CheckingList;
            List<Business> bList = currentAccount.BusinessList;

            while (true)
            {
                Console.WriteLine("Please select an account to transfer to:\n" +
                              "Checking Account:");
                //show the list of checking account
                for (int i = 0; i < checkingCount; i++)
                {
                    //only show the account that user haven's chosen
                    if (type == 1 && from == i)
                    {
                        //not show it
                    }
                    else
                        Console.WriteLine($"{i + 1}) {cList[i].AccountID} and Balance is {cList[i].Balance}");
                }

                //show the list of business account
                Console.WriteLine("\n" +
                                  "Business Account");
                for (int i = checkingCount; i < businessCount; i++)
                {
                    //since the number should follow by the checking Account
                    // so we need minus the checkingCount in order to point to correct index of Business Account List
                    //only show the account that user haven's chosen
                    if (type == 2 && from + checkingCount == i)
                    {
                        //not show it
                    }
                    else
                        Console.WriteLine($"{i + 1}) {bList[i - checkingCount].AccountID} and Balance is {bList[i - checkingCount].Balance}");
                }

                //or go back
                Console.WriteLine($"{businessCount + 1}) go back");

                // check if user enter valid input
                int input = validation.IsNum(Console.ReadLine());
                //same as in TransferFrom method but also need to check if user select the same account in TransferFrom
                if (input > 0 && input <= businessCount + 1 && input != from + businessCount + 1)
                {
                    //check type of account
                    if (input <= checkingCount)
                    {
                        // input less than checkingCoun
                        //it mean user transfer from checking account
                        to = input - 1;
                        return 1;
                    }
                    else if (input < businessCount + 1)
                    {
                        //user choose business account
                        to = input - businessCount + 1;
                        return 2;
                    }
                    else
                    {
                        to = 0;
                        return 0;
                    }

                }
            }
        }
    }
}
