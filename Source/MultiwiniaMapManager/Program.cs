using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MultiwiniaMapManager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!startupCheck())
            {
                Environment.Exit(1);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new wMainMenu());
        }

        static bool startupCheck()
        {
            bool result = true;

            //This program must run from the main Multiwinia directory
            //Check for "multiwinia.exe"
            if(!File.Exists("multiwinia.exe"))
            {
                MessageBox.Show("This program must be run from your Multiwinia directory!","Bad directory");
                result = false;
            }

            return result;
        }
    }
}
