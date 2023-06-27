using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Queries
{
    public static class QueriesExtensionMethods
    {
        public static string ConcatQueries(this string mainQuery, string whereClause = "", string hero = "", string tourneyType = "", int posHero = 0, int posVillain = 0, string regList = "")
        {

            string output = mainQuery;
            if (mainQuery.Contains("@whereClause"))
            {
                output = output.Replace("@whereClause", whereClause);
            }

            if (mainQuery.Contains("@hero"))
            {
                output = output.Replace("@hero", hero);
            }

            if (mainQuery.Contains("@tourneyType"))
            {
                output = output.Replace("@tourneyType", tourneyType);
            }

            if (mainQuery.Contains("@posHero"))
            {
                output = output.Replace("@posHero", posHero.ToString());
            }

            if (mainQuery.Contains("@posVillain"))
            {
                output = output.Replace("@posVillain", posVillain.ToString());
            }

            if (mainQuery.Contains("@regList"))
            {
                output = output.Replace("@regList", regList);
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
