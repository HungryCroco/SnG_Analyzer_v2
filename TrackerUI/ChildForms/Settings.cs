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
using TrackerLibrary.Queries;
//using static TrackerLibrary.Models;


namespace TrackerUI.ChildForms
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            
            
            

            ReadCurrentSettings();
            LoadDataBases();


            //LoadComboBoxes();
        }

        private void btnChooseRegFile_Click(object sender, EventArgs e)
        {
            labelChooseRegFile.Text = File_Connector.GetFullFilePath();
        }

        private void ReadCurrentSettings()
        {
            try
            {
                TrackerLibrary.Models.Settings currSettings = DataManager_Settings.ReadSettings();
                txtBoxServer.Text = currSettings.Server;
                txtBoxPort.Text = currSettings.Port;
                txtBoxUser.Text = currSettings.User;
                txtBoxPass.Text = currSettings.Password;

                txtBoxNosqlDb.Text = currSettings.NosqlDatabase;
                txtBoxSqlDb.Text = currSettings.SqlDatabase;
                txtBoxDbTypeRead.Text = currSettings.DbTypeRead;
                txtBoxDbTypeWrite.Text = currSettings.DbTypeWrite;

                txtBoxMinTourneys.Text = currSettings.MinTourney;
                labelChooseRegFile.Text = currSettings.RegFile;
                txtBoxHhSplitSize.Text = currSettings.HhSplitSize;
            }
            catch (Exception)
            {
                //Set to default
                txtBoxServer.Text = GlobalConfig.defaultServer;
                txtBoxPort.Text = GlobalConfig.defaultPort;
                txtBoxUser.Text = GlobalConfig.defaultUser;
                txtBoxPass.Text = GlobalConfig.defaultPass;

                txtBoxNosqlDb.Text = GlobalConfig.defaultNosqlDb;
                txtBoxSqlDb.Text = GlobalConfig.defaultSqlDb;
                txtBoxDbTypeRead.Text = GlobalConfig.defaultDbTypeRead;
                txtBoxDbTypeWrite.Text = GlobalConfig.defaultDbTypeWrite;

                txtBoxMinTourneys.Text = GlobalConfig.defaultMinTourney.ToString();
                labelChooseRegFile.Text = GlobalConfig.defaultRegList;
                txtBoxHhSplitSize.Text = GlobalConfig.defaultHhSplitSize.ToString();

            }

        }
        //private void LoadComboBoxes()
        //{
        //    Dictionary<string, string> mapComboBoxDataBaseWrite = new() { { "ALL", "ALL" } , { "SQL", "SQL" }, { "NoSQL", "NoSQL" } };
        //    cmbBox_DataBaseWrite.DataSource = new BindingSource(mapComboBoxDataBaseWrite, null);
        //    cmbBox_DataBaseWrite.DisplayMember = "Key";
        //    cmbBox_DataBaseWrite.ValueMember = "Value";

        //    Dictionary<string, string> mapComboBoxDataBaseRead = new() { { "SQL", "SQL" }, { "NoSQL", "NoSQL" } };
        //    cmbBox_DataBaseRead.DataSource = new BindingSource(mapComboBoxDataBaseRead, null);
        //    cmbBox_DataBaseRead.DisplayMember = "Key";
        //    cmbBox_DataBaseRead.ValueMember = "Value";
        //}
        private void SaveSettings(object sender, EventArgs e)
        {


            string currServer = txtBoxServer.Text;
            string currPort = txtBoxPort.Text;
            string currUser = txtBoxUser.Text;
            string currPass = txtBoxPass.Text;

            string currNosqlDb = txtBoxNosqlDb.Text;
            string currSqlDb = txtBoxSqlDb.Text;
            string currDbWrite = txtBoxDbTypeWrite.Text;
            string currDbRead = txtBoxDbTypeRead.Text;

            string currMinTourneys = txtBoxMinTourneys.Text;
            string currRegFile = labelChooseRegFile.Text;
            string currHhSplitSize = txtBoxHhSplitSize.Text;



            DataManager_Settings.WriteSettings(currServer, currPort, currUser, currPass, currNosqlDb, currSqlDb, currDbWrite, currDbRead ,currMinTourneys, currRegFile, currHhSplitSize);
        }

        private void LoadDataBases()
        {
            try
            {
                TrackerLibrary.Models.Settings currSettings = DataManager_Settings.ReadSettings();
                dataGridView_DataBases.DataSource = null;
                dataGridView_DataBases.DataSource = DB_InfoQueries.DB_Info_ExportDataGridView_AllDBs.GetView(currSettings.CurrentDbRead);

                dataGridView_DataBases.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_DataBases.BackgroundColor = Color.White;
                dataGridView_DataBases.AutoResizeDataGridView();
            }
            catch (Exception)
            {
            }
           
        }

        //private void SaveSettings(object sender, MouseEventArgs e)
        //{
        //    string currServer = txtBoxServer.Text;
        //    string currPort = txtBoxPort.Text;
        //    string currUser = txtBoxUser.Text;
        //    string currPass = txtBoxPass.Text;

        //    string currNosqlDb = txtBoxNosqlDb.Text;
        //    string currSqlDb = txtBoxSqlDb.Text;
        //    string currDbWrite = cmbBox_DataBaseWrite.SelectedValue != null ? cmbBox_DataBaseWrite.SelectedValue.ToString() : "ALL";
        //    string currDbRead = cmbBox_DataBaseRead.SelectedValue != null ? cmbBox_DataBaseRead.SelectedValue.ToString() : "SQL";

        //    string currMinTourneys = txtBoxMinTourneys.Text;
        //    string currRegFile = labelChooseRegFile.Text;
        //    string currHhSplitSize = txtBoxHhSplitSize.Text;

        //    DataManager_Settings.WriteSettings(currServer, currPort, currUser, currPass, currNosqlDb, currSqlDb, currDbWrite, currDbRead, currMinTourneys, currRegFile, currHhSplitSize);
        //}


        
    }
}
