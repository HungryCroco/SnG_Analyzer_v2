

using System.Diagnostics;

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
        /// Application's Name;
        /// </summary>
        public static string projectName = "SnG_Analyzer_v2";

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
        /// Path of the PreFlop Equity Array used to run PreFlop equity Calcs in EVCalculator;
        /// </summary>
        public static string pfEA = @"\EquityArray\eaPF_a.txt";

        /// <summary>
        /// Path of the .dll calculating th Hand Equities;
        /// </summary>
        public const string cppPokerOddsCalculatorDLL = @"PokerOddsCalculator_v4.dll";

        /// <summary>
        /// Active Player;
        /// </summary>
        public const string defaultHero = "IPray2Buddha";

        /// <summary>
        /// Tournament Type;
        /// </summary>
        public static string defaultTourneyType = "3-max";

        /// <summary>
        /// TextColor of UI-mainForm's Text;
        /// </summary>
        public static Color menuText = Color.FromArgb(94, 125, 95);

        /// <summary>
        /// BackGroundColor of UI-mainForm's buttons;
        /// </summary>
        public static Color btnDefault = Color.FromArgb(245, 249, 199);

        /// <summary>
        /// BackGroundColor of UI-mainForm's buttons while MouseHover event;
        /// </summary>
        public static Color btnMouseOver = Color.FromArgb(245, 249, 160);

        /// <summary>
        /// BackGroundColor of UI-mainForm's menu;
        /// </summary>
        public static Color menuBackGround = Color.FromArgb(233, 242, 225);

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

        /// <summary>
        /// Switches to the correct App's path depending on if it's runned in developer mode or it's being installed;
        /// </summary>
        /// <returns>App's Directory</returns>
        public static string GetMainFolderPath()
        {
            string baseDirectory = "";

            if (Debugger.IsAttached)
            {
                baseDirectory = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf(projectName) + projectName.Length);
            }
            else
            {
                baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            }

            return baseDirectory;
        }

    }
}