using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TrackerLibrary.Models.SettingsModel;

namespace TrackerLibrary.Models
{
    public class SettingsModel
    {

        public struct Settings
        {
            [JsonProperty("Server")]
            public string Server;

            [JsonProperty("Port")]
            public string Port;

            [JsonProperty("User")]
            public string User;

            [JsonProperty("Password")]
            public string Password;

            [JsonProperty("SqlDatabase")]
            public string SqlDatabase;

            [JsonProperty("NosqlDatabase")]
            public string NosqlDatabase;

            [JsonProperty("DbTypeWrite")]
            public string DbTypeWrite;

            [JsonProperty("DbTypeRead")]
            public string DbTypeRead;

            [JsonProperty("MinTourney")]
            public string MinTourney;

            [JsonProperty("RegFile")]
            public string RegFile;

            [JsonProperty("HhSplitSize")]
            public string HhSplitSize;

            [JsonProperty("CurrentDbRead")]
            public string CurrentDbRead;


            public Settings(string server, string port, string user, string pass, string nosqlDatabase, string sqlDatabase, string dbTypeWrite, string dbTypeRead, string minTourney, string regFile, string hhSplitSize)
            {
                Server = server;
                Port = port;
                User = user;
                Password = pass;
                

                SqlDatabase = sqlDatabase;
                NosqlDatabase = nosqlDatabase;
                DbTypeWrite = dbTypeWrite;
                DbTypeRead = dbTypeRead;

                MinTourney = minTourney;
                RegFile = regFile;
                HhSplitSize = hhSplitSize;

                CurrentDbRead = DbTypeRead == "SQL" ? sqlDatabase : nosqlDatabase;
            }
        }
    }
}
