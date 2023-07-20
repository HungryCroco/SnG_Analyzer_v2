using TrackerLibrary;
using TrackerLibrary.CRUD;
using TrackerLibrary.Queries;


namespace TrackerUI.ChildForms
{
    /// <summary>
    /// Settings-ChildForm;
    /// </summary>
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            
            
            ReadCurrentSettings();
            LoadDataBases();


            //LoadComboBoxes();
        }

        /// <summary>
        /// Button for opening new window and choosing a .txt File with REG's list;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChooseRegFile_Click(object sender, EventArgs e)
        {
            labelChooseRegFile.Text = File_Connector.GetFullFilePath();
        }

        /// <summary>
        /// Reading the current Settings;
        ///     - Opening the .txt File Settings, Reading and deserializing the String;
        ///     - If there is no Settings File, reading the default Settings from GlobalConfig;
        /// </summary>
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
                txtBoxTourneyType.Text = currSettings.TourneyType;
                txtBoxHero.Text = currSettings.ActivePlayer;
                
                

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
                txtBoxTourneyType.Text = GlobalConfig.defaultTourneyType.ToString();
                txtBoxHero.Text = GlobalConfig.defaultHero;
                

            }

        }
        
        /// <summary>
        /// Saving the current Settings to a new Settigns.txt file;
        /// This Method is executed by any change of any Settings;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            string currTourneyType = txtBoxTourneyType.Text;
            string currHero = txtBoxHero.Text;
            



            DataManager_Settings.WriteSettings(currServer, currPort, currUser, currPass, currNosqlDb, currSqlDb, currDbWrite, currDbRead ,currMinTourneys, currRegFile, currHhSplitSize, currHero, currTourneyType);
        }

        /// <summary>
        /// Loading all available Databases in the current Server in a DataView;
        /// </summary>
        private void LoadDataBases()
        {
            try
            {
                TrackerLibrary.Models.Settings currSettings = DataManager_Settings.ReadSettings();
                dataGridView_DataBases.DataSource = null;
                dataGridView_DataBases.DataSource = DB_InfoQueries.DB_Info_ExportDataGridView_AllDBs.GetView();

                dataGridView_DataBases.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView_DataBases.BackgroundColor = Color.White;
                dataGridView_DataBases.AutoResizeDataGridView();
            }
            catch (Exception)
            {
            }
           
        }

    }
}
