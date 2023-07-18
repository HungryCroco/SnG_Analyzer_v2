
//using TrackerLibrary.DataAccess;
using System.Configuration;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

namespace TrackerLibrary
{
    /// <summary>
    /// This class contains all default Settings necessary to run the application;
    /// </summary>
    public static class GlobalConfig
    {
        /// <summary>
        /// Default DataBase Type
        /// </summary>
        public static DataBaseType dbType = DataBaseType.SQL;

        /// <summary>
        /// Default Server in PostgreSQL;
        /// </summary>
        public static string defaultServer = "localhost";

        /// <summary>
        /// Default Port in PostgreSQL;
        /// </summary>
        public static string defaultPort = "5434";

        /// <summary>
        /// Default User in PostgreSQL;
        /// </summary>
        public static string defaultUser = "postgres";

        /// <summary>
        /// Default Password in PostgreSQL;
        /// </summary>
        public static string defaultPass = "dbpass";

        /// <summary>
        /// Default DataBase Type for Reading Queries;
        /// </summary>
        public static string defaultDbTypeRead = "SQL";

        /// <summary>
        /// Default DataBase Type for Writing(importing) Queries;
        /// </summary>
        public static string defaultDbTypeWrite = "ALL";

        /// <summary>
        /// Default DataBase Name for Reading Queries;
        /// </summary>
        public static string defaultNosqlDb = "sng_analyzer_json"; // sql_50s_2";

        /// <summary>
        /// Default DataBase Name for Writing(importing) Queries;
        /// </summary>
        public static string defaultSqlDb = "sng_analyzer_sql"; // sql_50s_2";

        /// <summary>
        /// Default TableName of NoSQL Main-Table;
        /// </summary>
        public static string tableName = "hands";

        /// <summary>
        /// Default TableName of NoSQL Main-sonb-Column;
        /// </summary>
        public static string columnName = "data";

        /// <summary>
        /// Default TableName of DashBoardModel Table;
        /// </summary>
        public static string dashboardTableName = "dashboard";

        /// <summary>
        /// Name of the Settigns Folder;
        /// </summary>
        public static string settingsFolder = "Settings";

        /// <summary>
        /// Name of the Settigns File;
        /// </summary>
        public static string settingsfileName = "Settings.txt";

        /// <summary>
        /// Base Directory of the Application
        /// </summary>
        private static string fullPath =  AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// Application's Name;
        /// </summary>
        private static string projectName = "SnG_Analyzer_v2";

        /// <summary>
        /// path of installations Folder;
        /// </summary>
        public static string projectDirectory = fullPath.Substring(0, fullPath.IndexOf(projectName) + projectName.Length);

        /// <summary>
        /// path ot Settings Folder;
        /// </summary>
        public static string settingsDirectory = projectDirectory + "\\Settings";

        /// <summary>
        /// Full path of Settings .txt File;
        /// </summary>
        public static string settingsPath = settingsDirectory + "\\" + settingsfileName;

        /// <summary>
        /// Deault HandHistory Split Size, to control the RAM ussage;
        /// </summary>
        public static int defaultHhSplitSize = 60000;

        /// <summary>
        /// deault minimum Tournament needed to show a Period;
        /// </summary>
        public static int defaultMinTourney = 500;

        /// <summary>
        /// Default Name of the RegList;
        /// </summary>
        public static string defaultRegList = "reglist_small.txt";

        /// <summary>
        /// Directory of the Reg List
        /// </summary>
        public static string regListDirectory = projectDirectory + "\\RegList";

        /// <summary>
        /// Full Reg List path;
        /// </summary>
        public static string regListPath = regListDirectory + "\\" + defaultRegList;

        /// <summary>
        /// Path of the PreFlop Equity Array used to run PreFlop equity Calcs in EVCalculator;
        /// </summary>
        public static string pfEA = @"C:\Users\tatsi\source\repos\Poker\SpinAnalyzer\eaPF_a.txt";

        /// <summary>
        /// 
        /// </summary>
        //public static string directoryRegFilter = @"C:\Users\tatsi\source\repos\Poker\SpinAnalyzer\settings";

        /// <summary>
        /// Concatenates a directory and fileName;
        /// </summary>
        /// <returns>Full File Path</returns>
        public static string FullFilePath(string _fileName, string _directory)
        {
            // TODO: REFACTORE: Maybe define path in app settings: return $"{ConfigurationManager.AppSettings["filePath"]}\\{fileName}";
            return $"{_directory}\\{_fileName}";
        }

        /// <summary>
        /// Get a ConnectionString To PostgreSQL for Creating DBs;
        /// </summary>
        /// <returns>ConnectionString to PostgreSQL;</returns>
        public static string GetConnectionString()
        {
            string connString = string.Format(@"Host={0};Port={1};User Id={2};Password={3}", GlobalConfig.defaultServer, GlobalConfig.defaultPort, GlobalConfig.defaultUser, GlobalConfig.defaultPass);

            return connString;
        }

        /// <summary>
        /// Get a ConnectionString To PostgreSQL for Creating Tables or Querieng;
        /// </summary>
        /// <returns>ConnectionString to PostgreSQL;</returns>
        public static string GetConnectionString(string db)
        {
            string connString = string.Format(@"Host={0};Port={1};User Id={2};Password={3};Database={4}", GlobalConfig.defaultServer, GlobalConfig.defaultPort, GlobalConfig.defaultUser, GlobalConfig.defaultPass, db);

            return connString;
        }
    }
}