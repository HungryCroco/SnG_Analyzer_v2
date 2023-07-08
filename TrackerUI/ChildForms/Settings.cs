using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.CRUD;
using TrackerLibrary.Models;


namespace TrackerUI.ChildForms
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            ReadCurrentSettings();
        }

        private void btnChooseRegFile_Click(object sender, EventArgs e)
        {
            labelChooseRegFile.Text = File_Connector.GetFullFilePath();
        }

        private void ReadCurrentSettings()
        {
            try
            {
                SettingsModel.Settings currSettings = File_CRUD_Settings.ReadSettings();
                txtBoxServer.Text = currSettings.Server;
                txtBoxPort.Text = currSettings.Port;
                txtBoxUser.Text = currSettings.User;
                txtBoxPass.Text = currSettings.Password;

                txtBoxMinTourneys.Text = currSettings.MinTorney;
                labelChooseRegFile.Text = currSettings.RegFile;
            }
            catch (Exception)
            {
                //Set to default
                txtBoxServer.Text = GlobalConfig.defaultServer;
                txtBoxPort.Text = GlobalConfig.defaultPort;
                txtBoxUser.Text = GlobalConfig.defaultUser;
                txtBoxPass.Text = GlobalConfig.defaultPass;

                txtBoxMinTourneys.Text = GlobalConfig.defaultMinTourney.ToString();
                labelChooseRegFile.Text = GlobalConfig.defaultRegList;

            }
            
        }

        private void SaveSettings(object sender, EventArgs e)
        {
            string currServer = txtBoxServer.Text;
            string currPort = txtBoxPort.Text;
            string currUser = txtBoxUser.Text;
            string currPass = txtBoxPass.Text;

            string currMinTourneys = txtBoxMinTourneys.Text;
            string currRegFile = labelChooseRegFile.Text;

            File_CRUD_Settings.WriteSettings(currServer, currPort, currUser, currPass, currMinTourneys, currRegFile);
        }
    }
}
