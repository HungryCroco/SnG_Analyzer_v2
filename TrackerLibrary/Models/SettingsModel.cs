using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            [JsonProperty("MinTorney")]
            public string MinTorney;

            [JsonProperty("RegFile")]
            public string RegFile;


            public Settings(string server, string port, string user, string pass, string minTorney, string regFile)
            {
                Server = server;
                Port = port;
                User = user;
                Password = pass;
                MinTorney = minTorney;
                RegFile = regFile;
            }
        }
    }
}
