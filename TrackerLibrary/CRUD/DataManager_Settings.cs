using System.Text.Json;
using TrackerLibrary.Models;

namespace TrackerLibrary.CRUD
{
    /// <summary>
    /// This class contains all the Methods needed to calculate a Settings UI;
    /// </summary>
    public static class DataManager_Settings
    {
        /// <summary>
        /// Reads all Settings from the .txt File. If .txt File is not available, reads the default Settings from the GlobalConfig;
        /// </summary>
        /// <returns>SettingsModel, struct containing all user-defined Settings;</returns>
        public static Settings ReadSettings()
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(new SettingsConverter());

            Settings settings = (Settings)File_Connector.ReadJsonFromFile<Settings>(GlobalConfig.GetMainFolderPath() + "\\Settings\\Settings.txt", options);

            return settings;

        }

        /// <summary>
        /// Writes all Settings from the Setting's UI to .txt File.
        /// </summary>
        /// <param name="server">PostgreSQL Server;</param>
        /// <param name="port">PostgreSQL Port;</param>
        /// <param name="user">PostgreSQL UserName;</param>
        /// <param name="pass">PostgreSQL Password;</param>
        /// <param name="nosqlDb">>Name of the current NoSQL-Database;</param>
        /// <param name="sqlDb">Name of the current SQL-Database;</param>
        /// <param name="dbWrite">The choosen type of DataBase used for importing Hands;</param>
        /// <param name="dbRead">The choosen type of DataBase used for quering;</param>
        /// <param name="minTourney">Minimum amount of Tournaments/period required to show the period's info in the UI;</param>
        /// <param name="regFile">Current reg-File used to filter Quieries by REG/FISH;</param>
        /// <param name="hhSplitSize">Amount of Hands to be Carculated and Imported at the same time;</param>
        /// <param name="hero">Active Player;</param>
        /// <param name="tourneyType">Tournament Type;</param>
        public static void WriteSettings(string server, string port, string user, string pass, string nosqlDb, string sqlDb, string dbWrite, string dbRead, string minTourney, string regFile, string hhSplitSize, string hero, string tourneyType)
        {
            // Read the Settings
            Settings settings = new(server, port, user, pass, nosqlDb, sqlDb, dbWrite, dbRead, minTourney, regFile, hhSplitSize, hero, tourneyType);

            try
            {
                // Creates Directory Settings if not available
                Directory.CreateDirectory(GlobalConfig.GetMainFolderPath() + "\\Settings");
            }
            catch (Exception)
            {
            }

            //Serializes Settings as JSON;
            var options = new JsonSerializerOptions();
            options.Converters.Add(new SettingsConverter());

            // Writes Settings to .txt;
            File_Connector.WriteToFileAsJSON(GlobalConfig.GetMainFolderPath() + "\\Settings\\Settings.txt", settings, options);

        }


    }
}
