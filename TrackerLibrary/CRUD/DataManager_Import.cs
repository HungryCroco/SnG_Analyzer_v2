using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary.CRUD
{
    public static class DataManager_Import
    {
        public static void RequestImport(string filePath, SettingsModel.Settings settings, [Optional] ProgressBar pb)
        {
            string entireHH = filePath.ReadFileReturnString();

            string[] splitString = entireHH.SplitStringBySize(int.Parse(settings.HhSplitSize));

            int pbValue = 0;

            foreach (string hh in splitString) 
            {
                
                

                pb.Invoke(new Action(() =>
                {
                    pb.Minimum = 1;
                    pb.Maximum = splitString.Length;
                    pb.Value = pbValue;
                }));
                
                List<Hand> hands = HHReader.ReadHands(hh);

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

                ++pbValue;
            }
        }
    }
}
