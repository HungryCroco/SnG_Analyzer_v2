
//using TrackerLibrary.DataAccess;
using System.Configuration;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

namespace TrackerLibrary
{
    public partial class GlobalConfig
    {
        public static DataBaseType dbType = DataBaseType.SQL;
        public static string defaultServer = "localhost";
        public static string defaultPort = "5434";
        public static string defaultUser = "postgres";
        public static string defaultPass = "dbpass";

        public static string dbName = "sql_50s_5"; // sql_50s_2";
        public static string tableName = "hands";
        public static string columnName = "data";

        public static string settingsFolder = "Settings";
        public static string settingsfileName = "Settings.txt";

        private static string fullPath =  AppDomain.CurrentDomain.BaseDirectory;
        private static string projectName = "SnG_Analyzer_v2";
        public static string projectDirectory = fullPath.Substring(0, fullPath.IndexOf(projectName) + projectName.Length);
        public static string settingsDirectory = projectDirectory + "\\Settings";
        public static string settingsPath = settingsDirectory + "\\" + settingsfileName;

        public static int defaultMinTourney = 500;
        public static string defaultRegList = "reglist_small.txt";
        private static string regListDirectory = projectDirectory + "\\RegList";
        public static string regListPath = regListDirectory + "\\" + defaultRegList;

        public static string dashBoardList = "dashboard.txt";
        //public static string hhFileName = "EV_Calcs.txt";
        public static string hhFileName = "ps_50s.txt";
        public static string pfEA = @"C:\Users\tatsi\source\repos\Poker\SpinAnalyzer\eaPF_a.txt";
        public static string directoryHH = @"C:\Users\tatsi\source\repos\Poker\HH SpinAndGo";
        public static string directoryRegFilter = @"C:\Users\tatsi\source\repos\Poker\SpinAnalyzer\settings";
        public static string temp = @"C:\Users\tatsi\source\repos\Poker\SpinAnalyzer\temp";

        //public static void InitializeConnections()
        //{
        //    PostgresqlConnector sql = new PostgresqlConnector();
        //}

        //public static string CnnString(string name)
        //{
        //    return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        //}

        public static string FullFilePath(string _fileName, string _directory)
        {
            // TODO: REFACTORE: Maybe define path in app settings: return $"{ConfigurationManager.AppSettings["filePath"]}\\{fileName}";
            return $"{_directory}\\{_fileName}";
        }

        public static string GetConnectionString()
        {
            string connString = string.Format(@"Host={0};Port={1};User Id={2};Password={3}", GlobalConfig.defaultServer, GlobalConfig.defaultPort, GlobalConfig.defaultUser, GlobalConfig.defaultPass);

            return connString;
        }
        public static string GetConnectionString(string db)
        {
            string connString = string.Format(@"Host={0};Port={1};User Id={2};Password={3};Database={4}", GlobalConfig.defaultServer, GlobalConfig.defaultPort, GlobalConfig.defaultUser, GlobalConfig.defaultPass, db);

            return connString;
        }
    }
}