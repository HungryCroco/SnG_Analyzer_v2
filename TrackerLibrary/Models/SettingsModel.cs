using Newtonsoft.Json;


namespace TrackerLibrary.Models
{


    /// <summary>
    /// Contains all user-defined Settings powering up the Application;
    /// </summary>
    public struct Settings
    {
        /// <summary>
        /// PostgreSQL Server;
        /// </summary>
        [JsonProperty("Server")]
        public string Server;

        /// <summary>
        /// PostgreSQL Port;
        /// </summary>
        [JsonProperty("Port")]
        public string Port;

        /// <summary>
        /// PostgreSQL UserName;
        /// </summary>
        [JsonProperty("User")]
        public string User;

        /// <summary>
        /// PostgreSQL Password;
        /// Would be good to hide it, however the use-case of this Application doesn't require safety;
        /// </summary>
        [JsonProperty("Password")]
        public string Password;

        /// <summary>
        /// Name of the current SQL-Database; 
        /// </summary>
        [JsonProperty("SqlDatabase")]
        public string SqlDatabase;

        /// <summary>
        /// Name of the current NoSQL-Database; 
        /// </summary>
        [JsonProperty("NosqlDatabase")]
        public string NosqlDatabase;

        /// <summary>
        /// The choosen type of DataBase used for importing Hands:
        ///     - ALL - SQL and NoSQL;
        ///     - SQL - only SQL;
        ///     - NoSQL - only NoSQL;
        /// </summary>
        [JsonProperty("DbTypeWrite")]
        public string DbTypeWrite;

        /// <summary>
        /// The choosen type of DataBase used for quering:
        ///     - SQL - only SQL;
        ///     - NoSQL - only NoSQL;
        /// </summary>
        [JsonProperty("DbTypeRead")]
        public string DbTypeRead;

        /// <summary>
        /// Minimum amount of Tournaments/period required to show the period's info in the UI; 
        /// ToDo: currently not implemented;
        /// </summary>
        [JsonProperty("MinTourney")]
        public string MinTourney;

        /// <summary>
        /// Current reg-File used to filter Quieries by REG/FISH;
        /// </summary>
        [JsonProperty("RegFile")]
        public string RegFile;

        /// <summary>
        /// Amount of Hands to be Carculated and Imported at the same time; More Hand = more RAM  needed and faster total import time;
        /// </summary>
        [JsonProperty("HhSplitSize")]
        public string HhSplitSize;

        /// <summary>
        /// Choosen DateBase to be quered from;
        /// </summary>
        [JsonProperty("CurrentDbRead")]
        public string CurrentDbRead;

        /// <summary>
        /// Active Player;
        /// </summary>
        [JsonProperty("ActivePlayer")]
        public string ActivePlayer;

        /// <summary>
        /// TournamentType;
        /// </summary>
        [JsonProperty("TourneyType1")]
        public string TourneyType;


        /// <summary>
        /// Contains all user-defined Settings powering up the Application;
        /// </summary>
        /// <param name="server">PostgreSQL Server;</param>
        /// <param name="port">PostgreSQL Port;</param>
        /// <param name="user">PostgreSQL UserName;</param>
        /// <param name="pass">PostgreSQL Password;</param>
        /// <param name="nosqlDatabase">Name of the current NoSQL-Database;</param>
        /// <param name="sqlDatabase">Name of the current SQL-Database;</param>
        /// <param name="dbTypeWrite">The choosen type of DataBase used for importing Hands;</param>
        /// <param name="dbTypeRead">The choosen type of DataBase used for quering;</param>
        /// <param name="minTourney">Minimum amount of Tournaments/period required to show the period's info in the UI;</param>
        /// <param name="regFile">Current reg-File used to filter Quieries by REG/FISH;</param>
        /// <param name="hhSplitSize">Amount of Hands to be Carculated and Imported at the same time;</param>
        /// <param name="ActivePlayer">active Player</param>
        public Settings(string server, string port, string user, string pass, string nosqlDatabase, string sqlDatabase, string dbTypeWrite, string dbTypeRead, string minTourney, string regFile, string hhSplitSize, string activePlayer, string tourneyType)
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

            ActivePlayer = activePlayer;
            TourneyType = tourneyType;
        }


    }
}
