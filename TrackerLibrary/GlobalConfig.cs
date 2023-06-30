
//using TrackerLibrary.DataAccess;
using System.Configuration;

namespace TrackerLibrary
{
    public partial class GlobalConfig
    {
        public static string server = "localhost";
        public static string port = "5434";
        public static string user = "postgres";
        public static string pass = "dbpass";

        public static string dbName = "test_com8";
        public static string tableName = "hands";
        public static string columnName = "data";


        public static int minTourney = 500;
        public static string regList = "reglist_small.txt";
        public static string dashBoardList = "dashboard.txt";
        //public static string hhFileName = "EV_Calcs.txt";
        public static string hhFileName = "ps_com.txt";
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
    }
}