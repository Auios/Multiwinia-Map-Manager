
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
        [STAThread]
        static void Main()
        {
#if (!DEBUG)
            if (!startupCheck())
            {
                Environment.Exit(1);
            }
#endif
            setupFolders();

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

        static void setupFolders()
        {
            if(!Directory.Exists("data/levels-disabled"))
                Directory.CreateDirectory("data/levels-disabled");
            if (!Directory.Exists("data/levels"))
                Directory.CreateDirectory("data/levels");
        }
    }
}
