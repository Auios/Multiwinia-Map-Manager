﻿using System;
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
                ProcessStartInfo processInfo = new ProcessStartInfo
                {
                    FileName = "Multiwinia.exe"
                };
                Process.Start(processInfo);
                //Application.Exit();
            }
            catch(Exception)
            {
                MessageBox.Show("Multiwinia.exe not found!\nTo fix this error you must run this program from the same directory that contains Multiwinia.exe\n", "Bad directory");
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
            foreach (var item in lstbxEnabled.SelectedItems)
            {
                string fileName = item.ToString();
                File.Move("data\\levels\\" + fileName, "data\\levels-disabled\\" + fileName);
            }
            refreshLists();
        }

        private void btnEnable_Click(object sender, EventArgs e)
        {
            foreach(var item in lstbxDisabled.SelectedItems)
            {
                string fileName = item.ToString();
                File.Move("data\\levels-disabled\\" + fileName, "data\\levels\\" + fileName);
            }
            refreshLists();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog
            {
                InitialDirectory = "C:\\",
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
                RestoreDirectory = true,
                Multiselect = true
            };

            if(fd.ShowDialog() == DialogResult.OK)
            {
                foreach(string fileName in fd.FileNames)
                {
                    try
                    {
                        string fullFileName = fileName.Substring(fileName.LastIndexOf("\\")).Remove(0, 1);
                        File.Copy(fullFileName, "data\\levels\\" + fullFileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                refreshLists();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstbxEnabled.SelectedIndex >= 0)
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to delete\n" + lstbxEnabled.Items[lstbxEnabled.SelectedIndex].ToString(),"Warning",MessageBoxButtons.OKCancel);
                if(dr == DialogResult.OK)
                    File.Delete("data\\levels\\" + lstbxEnabled.Items[lstbxEnabled.SelectedIndex].ToString());
            }

            if (lstbxDisabled.SelectedIndex >= 0)
            {
                File.Delete("data\\levels-disabled\\" + lstbxDisabled.Items[lstbxDisabled.SelectedIndex].ToString());
            }
            refreshLists();
        }

        private void lstbxEnabled_Enter(object sender, EventArgs e) => lstbxDisabled.ClearSelected();
        private void lstbxDisabled_Enter(object sender, EventArgs e) => lstbxEnabled.ClearSelected();
    }
}
