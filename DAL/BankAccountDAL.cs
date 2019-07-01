using Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /*
     * This class is connect to database relate to any bank account
     * like
     * checking
     * business
     * Loan
     * Term Deposit
     */
    public class BankAccountDAL
    {
        private static string path = System.AppContext.BaseDirectory;
        private string DALPath = path.Substring(0, path.Length - 6) + "BankAccount\\";
        private  CurrentAccount currentAccount;
        public BankAccountDAL()
        {
            currentAccount = new CurrentAccount();
            //after sign in,add username as file name to path
            DALPath += (CurrentAccount.Username + ".json");
            //check if the Account folder exist, if not create it
            Directory.CreateDirectory(Path.GetDirectoryName(DALPath));
            //check if the file not exist
            if (!File.Exists(DALPath))
            { 
                //create one
                SaveFile();
            }
        }
        //fetch the user information form json file
        public void FetchInfo()
        {

            //check if the BankAccount folder exist, if not create it
            Directory.CreateDirectory(Path.GetDirectoryName(DALPath));

            //check if the file exist
            if (File.Exists(DALPath))
            {
                //yes, deserialize it  
                ReadFromFile();
            }
        }

        //create new json file for database and save data to it
        public void SaveFile()
        {
            try
            {
                //open or create file if it's not exit
                FileStream stream = new FileStream(DALPath, FileMode.OpenOrCreate);
                //after create new file,close it
                stream.Close();
                //serialize the informationto file
                SaveToFile(); ;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        //serialize to json file
        public void SaveToFile()
        {
            //setup JsonSerializer
            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter sw = new StreamWriter(DALPath))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, currentAccount);

            }
        }

        //read the context from the json file
        public void ReadFromFile()
        {
            currentAccount = JsonConvert.DeserializeObject<CurrentAccount>(File.ReadAllText(DALPath));
        }
    }
}
