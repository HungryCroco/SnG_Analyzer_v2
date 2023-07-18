using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
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

            int pbValue = 0; // ProgressBar's updater;

            foreach (string hh in splitString) 
            {

                // Update the ProgressBar
                if (pb != null)
                {
                    pb.Invoke(new Action(() =>
                    {
                        pb.Minimum = 1;
                        pb.Maximum = splitString.Length;
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

            Console.WriteLine("Import Finished!");
        }
    }
}
