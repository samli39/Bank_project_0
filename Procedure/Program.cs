using ConsoleUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procedure
{
    class Program
    {
        /*
           The main method is to process action from the user
        */
        static void Main(string[] args)
        {
            //a variable of user input
            int result;
            CUI ui = new CUI();

            //the while loop is for people who logout
            while (true)
            {
                //start the program and show main menu
                result = ui.HomePage();

                if (result == 1)
                {
                    // 1 == sign in
                    //if successfully sign in,redirect to Home Page
                    if (ui.SignInPage())
                        ui.MainMenuPage();

                }
                else if (result == 2)
                {
                    //2 == register an account (not bank account)
                    // redirect to register consoleUI
                    ui.RegisterPage();
                }
                // situations that code will reach here.
                // 1.After register an account,require user to sign in
                // 2.some bug, even though result not == 1 || 2, require user to make a action again
                // 3.user logout account.

            }//end of while loop

        }
    }
}
