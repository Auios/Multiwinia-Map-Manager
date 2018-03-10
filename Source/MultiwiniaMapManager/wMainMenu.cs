using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace MultiwiniaMapManager
{
    public partial class wMainMenu : Form
    {
        public wMainMenu()
        {
            InitializeComponent();
            refreshLists();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessStartInfo processInfo = new ProcessStartInfo();
                processInfo.FileName = "Multiwinia.exe";
                Process.Start(processInfo);
                Application.Exit();
            }
            catch(Exception)
            {
                MessageBox.Show("Multiwinia.exe not found!\nTo fix this error run this program in the same directory with Multiwinia.exe\n", "Bad directory");
            }
        }

        private void refreshLists()
        {
            lstbxEnabled.Items.Clear();
            lstbxDisabled.Items.Clear();

            String[] enabledMaps = Directory.GetFiles("data\\levels");
            for (int i = 0; i < enabledMaps.Length; i++)
            {
                string fileName = enabledMaps[i].Substring(enabledMaps[i].LastIndexOf("\\")).Remove(0, 1);
                lstbxEnabled.Items.Add(fileName);
            }

            String[] disabledMaps = Directory.GetFiles("data\\levels-disabled");
            for (int i = 0; i < disabledMaps.Length; i++)
            {
                string fileName = disabledMaps[i].Substring(disabledMaps[i].LastIndexOf("\\")).Remove(0,1);
                lstbxDisabled.Items.Add(fileName);
            }
        }

        private void btnDisable_Click(object sender, EventArgs e)
        {
            int index = lstbxEnabled.SelectedIndex;
            if(index >= 0)
            {
                string fileName = lstbxEnabled.Items[index].ToString();
                File.Move("data\\levels\\" + fileName, "data\\levels-disabled\\" + fileName);
            }
            refreshLists();
        }

        private void btnEnable_Click(object sender, EventArgs e)
        {
            int index = lstbxDisabled.SelectedIndex;
            if (index >= 0)
            {
                string fileName = lstbxDisabled.Items[index].ToString();
                File.Move("data\\levels-disabled\\" + fileName, "data\\levels\\" + fileName);
            }
            refreshLists();
        }
    }
}
