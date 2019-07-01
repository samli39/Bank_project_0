using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    /**
      * This class is for saving information of current login account.
      */
    [JsonObject(MemberSerialization.OptIn)]
    public class CurrentAccount
    {
        [JsonProperty]
        public static string Firstname { get; set; }
        [JsonProperty]
        public static string Lastname { get; set; }
        [JsonProperty]
        public static string Username { get; set; }
        




        [JsonProperty]
        private static List<Checking> checkingList = new List<Checking>();
        [JsonProperty]
        private static List<Business> businessList = new List<Business>();
        
        [JsonProperty]
        private static List<Loan> loanList = new List<Loan>();
        [JsonProperty]
        private static List<TermDeposit> tDList = new List<TermDeposit>();

        public List<Checking> CheckingList
        {
            get => checkingList; set
            {
                CheckingList = value;
            }
        }

       public List<Business> BusinessList
        {
            get => businessList; set
            {
                BusinessList = value;
            }
        }

        

        public List<Loan> LoanList
        {
            get => loanList; set
            {
                LoanList = value;
            }
        }
        public List<TermDeposit> TDList
        {
            get => tDList; set
            {
                TDList = value;
            }
        }



    }
}
