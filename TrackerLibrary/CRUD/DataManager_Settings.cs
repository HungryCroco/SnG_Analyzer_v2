using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary.CRUD
{
    public class DataManager_Settings
    {
        public static SettingsModel.Settings ReadSettings()
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(new SettingsConverter());

            SettingsModel.Settings settings = (SettingsModel.Settings)File_Connector.ReadJsonFromFile<SettingsModel.Settings>(GlobalConfig.settingsPath, options);

            return settings;

        }

        public static void WriteSettings(string server, string port, string user, string pass, string nosqlDb, string sqlDb, string dbWrite, string dbRead, string minTourney, string regFile, string hhSplitSize)
        {

            SettingsModel.Settings settings = new(server, port, user, pass, nosqlDb, sqlDb, dbWrite, dbRead, minTourney, regFile, hhSplitSize);

            try
            {
                Directory.CreateDirectory(GlobalConfig.settingsDirectory);
            }
            catch (Exception)
            {

                throw;
            }

            var options = new JsonSerializerOptions();
            options.Converters.Add(new SettingsConverter());

            File_Connector.WriteToFileAsJSON(GlobalConfig.settingsPath, settings, options);

        }


    }
}
