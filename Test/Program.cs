using System.Diagnostics;
using TrackerLibrary;
using TrackerLibrary.CRUD;
using TrackerLibrary.Models;




namespace Test
{
    internal class Program
    {
        public const string pfEA = @"C:\Users\tatsi\source\repos\Poker\SpinAnalyzer\eaPF_a.txt";
        private static float[,,,] ea = EVCalculator.ImportDLL.ReadEAFromFileAsFloatArray(pfEA);

        static string directoryHH = @"C:\Users\tatsi\source\repos\Poker\HH SpinAndGo";
        static string hhFileName = "ps_com.txt"; // ps_com.txt";//"EV_Calcs.txt";
        //static string hhFileName = "EV_Calcs.txt";
        static List<string> hhAsStringArr = GlobalConfig.FullFilePath(hhFileName, directoryHH).ReadFileReturnListOfString();
        static string entireHH = GlobalConfig.FullFilePath(hhFileName, directoryHH).ReadFileReturnString();
        static List<Hand> allHands = new List<Hand>();
        

        static void Main(string[] args)
        {
            //Console.WriteLine("start");
            Stopwatch watch = new();
            watch.Start();

            //HHReader.ReadHandIdBySite(entireHH);

            float res1 = 0;
            float res2 = 0;
            float res3 = 0;

            var splitString = entireHH.SplitStringBySize(60000);

            foreach (var hh in splitString)
            {    
                NoSQL_Connector.InsertToNoSqlDb(GlobalConfig.dbName, GlobalConfig.tableName, GlobalConfig.columnName, HHReader.ReadHands(hh));
            }
            

            //EVCalculator.ImportDLL.CalculateOdds_2Hands_PF(52, 51, 50, 49, ref res1, ref res2, ref res3, ea);


            Console.WriteLine("Total Time: " + watch.ElapsedMilliseconds/1000 + "s");


            if (true)
            {

            }
        }
    }
}






