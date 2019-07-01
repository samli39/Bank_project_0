using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class TermDepositBL
    {
        CurrentAccount current = new CurrentAccount();
        BankAccountDAL dal = new BankAccountDAL();
        public void CreateTermDeposit(double amount)
        {
            TermDeposit newTD = new TermDeposit()
            {
                ID = new IDgenerator().GenerateID(),
                Amount = Math.Round(amount * 1.1),
                Maturity = DateTime.Now.AddDays(1)
            };

            current.TDList.Add(newTD);
            dal.SaveFile();
            
        }
        public void CloseDeposit(int input, int checking)
        {
            //deposit to checking first
            current.CheckingList[checking].Deposit(current.TDList[input].Amount);
            //create transaction for checking account
            Transaction trans = new Transaction()
            {
                TransID = new IDgenerator().GenerateID(),
                TransType = "deposit",
                Target = $"from Term deposit Account {current.TDList[input].ID}",
                Amount = current.TDList[input].Amount,
                Date = DateTime.Now
            };
            //add to the list
            current.CheckingList[checking].TransList.Add(trans);
            //remove the Term Deposit from list
            current.TDList.RemoveAt(input);
            dal.SaveFile();
        }
    }
}


