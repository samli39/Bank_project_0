using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    /*
     * All database related action including
     *  checking account,
     *  Business account
     *  loan
     *  term deposit
     */
    public class BankAccountBL
    {
        private BankAccountDAL dal = new BankAccountDAL();
        private CurrentAccount currentAccount = new CurrentAccount();
        private IDgenerator IDgen = new IDgenerator();
        //fetch information from database after sign in
        public bool FetchInfo()
        {
            try
            {
                dal.FetchInfo();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot fetch information from database, Please sign in again");
                return false;
            }

            return true;
        }

        /*
         * chekcing account related
         */
        public void OpenChecking()
        {
            //create account id 
            string id;
            //loop unitl the new id didn't exist
            while (true)
            {
                id = IDgen.GenerateID();
                if (new Validation().UniqueID(id))
                    break;
            }
                
            
            
            //create a checking object
            Checking ac = new Checking()
            {
                AccountID = id,
                Date=DateTime.Now
            };

            //add to currentAccount
            currentAccount.CheckingList.Add(ac);

            //save to json file
            dal.SaveToFile();
            Console.WriteLine("new chekcing account created!!\n");
        }
        public void CloseChecking(int index)
        {
            //remove checking account from list
            currentAccount.CheckingList.RemoveAt(index);
            dal.SaveFile();
        }

        public void CheckingDeposit(int index,double amount)
        {
            
            //create new transaction
            Transaction trans = new Transaction()
            {
                TransID = IDgen.GenerateID(),
                TransType = "Deposit",
                Amount = amount,
                Date = DateTime.Now
            };
            //save to list
            currentAccount.CheckingList[index].Deposit(amount);
            currentAccount.CheckingList[index].TransList.Add(trans);
            // save to json file
            dal.SaveFile();
        }

        public void CheckingWithdraw(int index, double amount)
        {
            //create new transaction
            Transaction trans = new Transaction()
            {
                TransID = IDgen.GenerateID(),
                TransType = "Withdraw",
                Amount = amount,
                Date = DateTime.Now
            };
            //save to list
            if (currentAccount.CheckingList[index].Withdraw(amount))
            {
                currentAccount.CheckingList[index].TransList.Add(trans);
                // save to json file
                dal.SaveFile();
            }
            
        }

        /*
         * Business Account related
         */
        public void OpenBusiness()
        {
            //create account id 
            string id;
            //loop unitl the new id didn't exist
            while (true)
            {
                id = IDgen.GenerateID();
                if (new Validation().UniqueID(id))
                    break;
            }

            //create business account
            Business ac = new Business()
            {
                AccountID = id,
                Date = DateTime.Now
            };

            //add to currentAccount
            currentAccount.BusinessList.Add(ac);

            //save to json file
            dal.SaveToFile();
            Console.WriteLine("new business account created!!\n");
        }
        public void CloseBusiness(int index)
        {
            //remove checking account from list
            currentAccount.BusinessList.RemoveAt(index);
            dal.SaveFile();
        }

        public void BusinessDeposit(int index, double amount)
        {
            //create new transaction
            Transaction trans = new Transaction()
            {
                TransID = IDgen.GenerateID(),
                TransType = "Deposit",
                Amount = amount,
                Date = DateTime.Now
            };
            //save to list
            currentAccount.BusinessList[index].Deposit(amount);
            currentAccount.BusinessList[index].TransList.Add(trans);
            // save to json file
            dal.SaveFile();
        }
        public void BusinessWithdraw(int index, double amount)
        {
            //create new transaction
            Transaction trans = new Transaction()
            {
                TransID = IDgen.GenerateID(),
                TransType = "Withdraw",
                Amount = amount,
                Date = DateTime.Now
            };
            //save to list
            if (currentAccount.BusinessList[index].Withdraw(amount))
            {
                currentAccount.BusinessList[index].TransList.Add(trans);
                // save to json file
                dal.SaveFile();
            }
        }


        /*
         * Transfer related method
         */
        //type1 is the type of withdraw account
        //from is the index of withdraw account
        //type is the type of deposit account
        //to is the index of deposit account
        public void Transfer(int type1, int from, int type2, int to,double amount)
        {
            IBankAccount fromAc,toAc;

            //make the transaction
            //withdraw first
            //need to see if there is enough balance
            if (type1 == 1)
            {
                //transfer withdraw in checking account
                if (TransferWC(from, amount,out fromAc))
                {
                    //if account has enough funds
                    TransferDeposit(to, type2, amount, out toAc);

                }
                else
                {
                    //not enough funds
                    FailTransfer();
                    return;
                }

            }
            else
            {
                //withdraw in business account
                if (TransferWB(from, amount,out fromAc))
                {
                    //withdraw success
                    //check the deposit account type
                    TransferDeposit(to, type2, amount, out toAc);
                }
                else
                {
                    //not enough fund
                    FailTransfer();
                    return;
                }
            }

            //tansfer success
            //create transaction for those account 
            Transaction fromTrans = new Transaction()
            {
                TransID = IDgen.GenerateID(),
                TransType = "Withdraw",
                Target=$"To {toAc.AccountType} Account {toAc.AccountID}",
                Amount = amount,
                Date = DateTime.Now
            };
            //save to withdraw transaction list
            fromAc.TransList.Add(fromTrans);


            Transaction toTrans = new Transaction()
            {
                TransID = IDgen.GenerateID(),
                TransType = "Deposit",
                Target = $"from {fromAc.AccountType} Account {fromAc.AccountID}",
                Amount = amount,
                Date = DateTime.Now
            };
            //save to withdraw transaction list
            toAc.TransList.Add(toTrans);

            Console.WriteLine("Transfer Success!");
            dal.SaveFile();
        }

        

        private void FailTransfer()
        {
            Console.WriteLine("Fail to transfer, Please chekc if there is enough funds in account");
        }

        //withdraw from checking account for transfer
        private bool TransferWC(int index, double amount,out IBankAccount fromAc)
        {
            fromAc = currentAccount.CheckingList[index];
            if (!fromAc.Withdraw(amount))
            {
                return false;
            }
            //success
            return true;
        }

        //withdraw from business account for transfer
        private bool TransferWB(int index, double amount,out IBankAccount fromAc)
        {
            fromAc = currentAccount.BusinessList[index];
            //check if it already overdraft or the amount of withdraw larger than balance
            if (!fromAc.Withdraw(amount))
            {
                
                return false;
            }
            return true;
        }

        private void TransferDeposit(int to, int type2, double amount, out IBankAccount toAc)
        {
            //check the deposit account type
            if (type2 == 1)
            {
                //deposit in checking account
                TransferDC(to, amount, out toAc);
            }
            else
            {
                TransferDB(to, amount, out toAc);
                //deposit in business Account
            }
        }


        private void TransferDC(int index, double amount, out IBankAccount toAc)
        {
            toAc = currentAccount.CheckingList[index];
            toAc.Deposit(amount);
            
        }
        private void TransferDB(int index, double amount, out IBankAccount toAc)
        {
            toAc = currentAccount.BusinessList[index];
            toAc.Deposit(amount);
            
        }
    }
   
}
