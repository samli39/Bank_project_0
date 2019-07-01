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
     * This class is for login account only,not Bank Account
     */
    public class AccountDAL
    {
        //databse path
        private static string path = System.AppContext.BaseDirectory;
        private static string DALPath = path.Substring(0, path.Length - 6) + "Account\\Account.json";
        //a list to save or read from json
        private static AccountList list;

        /*
         * check if the Account.json is exist,
         * if not create it
         */
        public AccountDAL()
        {
            //check if the Account folder exist, if not create it
            Directory.CreateDirectory(Path.GetDirectoryName(DALPath));

            //check if the file exist
            if (File.Exists(DALPath))
            {
                //yes, deserialize it
                ReadFromJson();

            }
            else
            {
                //if it is new user, create empty list
                list = new AccountList();
            }
        }



        public bool AccountRegister(Account ac)
        {
            try
            {
                //create file if it's not exit
                FileStream stream = new FileStream(DALPath, FileMode.OpenOrCreate);
                //after create it , close it
                stream.Close();
                //serialize the informationto file
                SaveToJson(ac);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine();
                return false;
            }

            return true;
        }

        public void ReadFromJson()
        {
            list = JsonConvert.DeserializeObject<AccountList>(File.ReadAllText(DALPath));
        }

        public void SaveToJson(Account ac)
        {
            // need to check if the username already exists
            foreach (var ele in list.AcList)
            {
                if (ele.Username.Equals(ac.Username))
                    throw new Exception("Username already exists.");
            }

            //add ac to the list
            list.AcList.Add(ac);

            //save to Json File
            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter sw = new StreamWriter(DALPath))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, list);

            }
        }

        //check if the username and password are match in database
        public bool AccountMatch(string username, string password)
        {
            foreach (var ele in list.AcList)
            {
                if (ele.Username.Equals(username) && ele.Password.Equals(password))
                {

                    //if both match, then save it to currentAccount
                    CurrentAccount.Firstname = ele.Firstname;
                    CurrentAccount.Lastname = ele.Lastname;
                    CurrentAccount.Username = ele.Username;
                    return true;
                }
            }
            return false;
        }
    }

}
