using System.Diagnostics;
using System.Runtime.InteropServices;
using TrackerLibrary.Models;

namespace TrackerLibrary.CRUD
{
    /// <summary>
    /// This class contains all the Methods needed to calculate an Import UI;
    /// </summary>
    public static class DataManager_Import
    {
        /// <summary>
        /// Run a SQL, NoSQL or both Queries(depending on choosen DBType) that will import the Hands available in the choosen .txt file to the selected DB/s;
        /// </summary>
        /// <param name="filePath">Full path of the file that will be imported;</param>
        /// <param name="settings">Settings;</param>
        /// <param name="pb">[Optional]ProgressBar, that would be ubpdated during the import;</param>
        public static void RequestImport(string filePath, Settings settings, [Optional] ProgressBar pb)
        {
            Console.WriteLine("Import Started!");
            //loading the entire file as a single string;
            string entireHH = filePath.ReadFileReturnString();

            //deviding the string by Set of Hands to decrease the needed Memory;
            string[] splitString = entireHH.SplitStringBySize(int.Parse(settings.HhSplitSize));

            Console.WriteLine("Amount of HandSets to be imported: " + splitString.Count());

            int pbValue = 1; // ProgressBar's updater;

            foreach (string hh in splitString) 
            {

                // Update the ProgressBar
                if (pb != null)
                {
                    pb.Invoke(new Action(() =>
                    {
                        pb.Minimum = 0;
                        pb.Maximum = splitString.Length+1;
                        pb.Value = pbValue;
                    }));
                }

                // Read the Hands
                List<Hand> hands = HHReader.ReadHands(hh);


                // Run the needed SQL/ NoSQL queries depending on the DBTypeWrite;
                if (settings.DbTypeWrite == "ALL")
                {
                    NoSQL_Connector.InsertHandsToNoSqlDb(settings.NosqlDatabase, GlobalConfig.tableName, GlobalConfig.columnName, hands);

                    SQL_Connector.ImportHandsToSqlDb(settings.SqlDatabase, hands);
                }
                else if (settings.DbTypeWrite == "SQL")
                {
                    SQL_Connector.ImportHandsToSqlDb(settings.SqlDatabase, hands);
                }
                else if (settings.DbTypeWrite == "NoSQL")
                {
                    NoSQL_Connector.InsertHandsToNoSqlDb(settings.NosqlDatabase, GlobalConfig.tableName, GlobalConfig.columnName, hands);
                }
                else
                {

                }


                // update the ProgressBar;
                ++pbValue;
            }

            Stopwatch watch = new();
            watch.Start();
            Console.WriteLine();
            Console.WriteLine("---");

            // Calculate default DashBoards; This Part could be refactured;
            if (settings.DbTypeWrite == "ALL")
            {
                
                string tempCurrDbRead = settings.CurrentDbRead;
                string tempDbTypeRead = settings.DbTypeRead;

                settings.CurrentDbRead = settings.SqlDatabase;
                settings.DbTypeRead = "SQL";
                settings.CurrentDbRead.DeleteTable(GlobalConfig.dashboardTableName);
                DataManager_DashBoard.RequestDashBoard(GlobalConfig.defaultHero, settings);
                Console.WriteLine($"{settings.CurrentDbRead} DashBoard-Page Calculated: " + watch.ElapsedMilliseconds / 1000.0 + "s");

                settings.CurrentDbRead = settings.NosqlDatabase;
                settings.DbTypeRead = "NoSQL";
                settings.CurrentDbRead.DeleteTable(GlobalConfig.dashboardTableName);
                DataManager_DashBoard.RequestDashBoard(GlobalConfig.defaultHero, settings);
                Console.WriteLine($"{settings.CurrentDbRead} DashBoard-Page Calculated: " + watch.ElapsedMilliseconds / 1000.0 + "s");
                settings.CurrentDbRead = tempCurrDbRead;
                settings.DbTypeRead = tempDbTypeRead;
            }
            else if (settings.DbTypeWrite == "SQL")
            {
                string temp = settings.CurrentDbRead;
                string tempDbTypeRead = settings.DbTypeRead;

                settings.CurrentDbRead = settings.SqlDatabase;
                settings.DbTypeRead = "SQL";
                settings.CurrentDbRead.DeleteTable(GlobalConfig.dashboardTableName);
                DataManager_DashBoard.RequestDashBoard(GlobalConfig.defaultHero, settings);
                Console.WriteLine($"{settings.CurrentDbRead} DashBoard-Page Calculated: " + watch.ElapsedMilliseconds / 1000.0 + "s");

                settings.CurrentDbRead = temp;
                settings.DbTypeRead = tempDbTypeRead;
            }
            else if (settings.DbTypeWrite == "NoSQL")
            {
                string temp = settings.CurrentDbRead;
                string tempDbTypeRead = settings.DbTypeRead;

                settings.CurrentDbRead = settings.NosqlDatabase;
                settings.DbTypeRead = "NoSQL";
                settings.CurrentDbRead.DeleteTable(GlobalConfig.dashboardTableName);
                DataManager_DashBoard.RequestDashBoard(GlobalConfig.defaultHero, settings);
                Console.WriteLine($"{settings.CurrentDbRead} DashBoard-Page Calculated: " + watch.ElapsedMilliseconds / 1000.0 + "s");

                settings.CurrentDbRead = temp;
                settings.DbTypeRead = tempDbTypeRead;
            }
            else
            {

            }

            // Update the ProgressBar
            if (pb != null)
            {
                pb.Invoke(new Action(() =>
                {
                    pb.Minimum = 0;
                    pb.Maximum = splitString.Length + 1;
                    pb.Value = pb.Maximum;
                }));
            }

            Console.WriteLine("-----");
            Console.WriteLine("-----");
            Console.WriteLine("-----");
            Console.WriteLine("Import Finished!");
        }
    }
}
