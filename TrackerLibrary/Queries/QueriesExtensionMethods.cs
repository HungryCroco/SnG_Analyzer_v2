

namespace TrackerLibrary.Queries
{
    /// <summary>
    /// This class containes methods necessary to concatenate Queries;
    /// </summary>
    public static class QueriesExtensionMethods
    {
        /// <summary>
        /// Replaces @param with param in the mainQuery;
        /// </summary>
        /// <param name="mainQuery">main Query;</param>
        /// <param name="whereClauseQuery"> General where Clause;</param>
        /// <param name="whereClauseHero">Hero's where Clause;</param>
        /// <param name="whereClauseVillain">Villain's where Clause;</param>
        /// <param name="hero">Hero's(Active Player's) NickName</param>
        /// <param name="cntPlayers">Number of players delath in the Hand;</param>
        /// <param name="tourneyType">Tournament Type;</param>
        /// <param name="posHero">Position of Hero as Number</param>
        /// <param name="posVillain">Position of Villain as Number</param>
        /// <param name="pfActors">preFlop Actors</param>
        /// <param name="regList">Reg List as SQL</param>
        /// <param name="date">Date</param>
        /// <param name="AI">AllIn</param>
        /// <param name="size">Size</param>
        /// <param name="effStack">Effective Stack</param>
        /// <param name="hcsId">Id of HoleCards</param>
        /// <param name="btnNULL">Button is NULL</param>
        /// <param name="seatActionHero">seatAction of Hero as position-column in SQL rel DB( like bbSeatActionID)</param>
        /// <param name="seatActionVillain">seatAction of Villain as position-column in SQL rel DB( like bbSeatActionID)</param>
        /// <returns>SQL/NoSQL Query with placed params, ready to be executed in PostgreSQL;</returns>
        public static string ConcatQueries(this string mainQuery, string whereClauseQuery = "", string whereClauseHero = "", string whereClauseVillain = "", string hero = "", int cntPlayers = 0, string tourneyType = "", int posHero = 0, int posVillain = 0, string pfActors = "", string regList = "", string date = "", string AI = "", string size = "", string effStack = "0", string hcsId = "", string btnNULL = "", string seatActionHero = "", string seatActionVillain = "")
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

            if (output.Contains("@pfActors"))
            {
                output = output.Replace("@pfActors", pfActors);
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

        /// <summary>
        /// Opens the RegList and gets all players from it;
        /// </summary>
        /// <param name="listName">Name of the Reg List;</param>
        /// <param name="isReg">TRUE =  is in Reg List, FALSE = is NOT in RegList;</param>
        /// <returns>SQL containing the players from the Reg List</returns>
        public static string ReadRegList(this string listName, bool isReg)
        {
            //Get the RegList as string;
            string regList = GlobalConfig.FullFilePath(listName, GlobalConfig.GetMainFolderPath() + "\\RegList").ReadFileReturnString();

            // Creates SQL-string for REGs or FISHes depending on bool isReg; 
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
