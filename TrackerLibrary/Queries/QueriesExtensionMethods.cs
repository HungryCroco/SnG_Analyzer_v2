using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Queries
{
    public static class QueriesExtensionMethods
    {
        public static string ConcatQueries(this string mainQuery, string whereClauseQuery = "", string whereClauseHero = "", string whereClauseVillain = "", string hero = "", int cntPlayers = 0, string tourneyType = "", int posHero = 0, int posVillain = 0, string regList = "", string date ="", string AI ="", string size ="", string effStack = "0",string hcsId = "", string btnNULL= "", string seatActionHero = "" , string seatActionVillain = "")
        {

            string output = mainQuery;
            if (output.Contains("@whereClauseQuery"))
            {
                output = output.Replace("@whereClauseQuery", whereClauseQuery);
            }

            if (output.Contains("@whereClauseHero"))
            {
                output = output.Replace("@whereClauseHero", whereClauseHero);
            }

            if (output.Contains("@whereClauseVillain"))
            {
                output = output.Replace("@whereClauseVillain", whereClauseVillain);
            }

            if (output.Contains("@hero"))
            {
                output = output.Replace("@hero", hero);
            }

            if (output.Contains("@cntPlayers"))
            {
                output = output.Replace("@cntPlayers", cntPlayers.ToString());
            }

            if (output.Contains("@tourneyType"))
            {
                output = output.Replace("@tourneyType", tourneyType);
            }

            if (output.Contains("@posHero"))
            {
                output = output.Replace("@posHero", posHero.ToString());
            }

            if (output.Contains("@posVillain"))
            {
                output = output.Replace("@posVillain", posVillain.ToString());
            }

            if (output.Contains("@regList"))
            {
                output = output.Replace("@regList", regList);
            }

            if (output.Contains("@date"))
            {
                output = output.Replace("@date", date);
            }

            if (output.Contains("@AI"))
            {
                output = output.Replace("@AI", AI);
            }

            if (output.Contains("@size"))
            {
                output = output.Replace("@size", size);
            }

            if (output.Contains("@es"))
            {
                output = output.Replace("@es", effStack);
            }

            if (output.Contains("@HCsId"))
            {
                output = output.Replace("@HCsId", hcsId);
            }

            if (output.Contains("@btnNULL"))
            {
                output = output.Replace("@btnNULL", btnNULL);
            }

            if (output.Contains("@seatActionHero"))
            {
                output = output.Replace("@seatActionHero", seatActionHero);
            }

            if (output.Contains("@seatActionVillain"))
            {
                output = output.Replace("@seatActionVillain", seatActionVillain);
            }

            return output;

        }

        public static string ReadRegList(this string listName, bool isReg)
        {

            var regList = GlobalConfig.FullFilePath(listName, GlobalConfig.directoryRegFilter).ReadFileReturnString();
            if (isReg)
            {
                return "in ( " + regList + " )";
            }
            else
            {
                return "not in ( " + regList + " )";
            }
        }
    }
}
