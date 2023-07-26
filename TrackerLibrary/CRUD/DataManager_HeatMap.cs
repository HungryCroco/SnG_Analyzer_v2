using System.Data;
using TrackerLibrary.Models;
using TrackerLibrary.Queries;


namespace TrackerLibrary.CRUD
{
    /// <summary>
    /// Contains extension Methods, necessery for calculating a HeatMap UI;
    /// As the HeatMap is planned to e run with many variables and is running fast(especially with SQL DB), there is no sense to save it to DB;
    /// </summary>
    public static class DataManager_HeatMap
    {
        /// <summary>
        ///  Run a SQL OR NoSQL Query(depending on choosen DBType) that groups Hands by active player's HoleCards and returns a JSON of StatsModel;
        /// </summary>
        /// <param name="query">Main Query;</param>
        /// <param name="hero">Active Player;</param>
        /// <param name="tourneyType">Tournament Type;</param>
        /// <param name="sinceDate">Date, that filters only Hands played after;</param>
        /// <param name="ai">Active Player is ALL-In or not;</param>
        /// <param name="size">Size of the Bet(if any)</param>
        /// <param name="vs">Villain is / is not in the REG's list</param>
        /// <param name="es">Effective Stack(the smallest stack of all active players)</param>
        /// <param name="settings">Settings</param>
        /// <returns></returns>
        public static List<StatsModel> RequestStatsModel_Heatmap(this Query query, string hero, string tourneyType, string sinceDate, string ai, string size, string vs, string es, TrackerLibrary.Models.Settings settings)
        {
            string isAi = settings.DbTypeRead == DataBaseType.NoSQL.GetDescription() ? ai : (ai == "0" ? "" : "^R[0-9]+ai$");
            return QueriesExtensionMethods.ConcatQueries(query.MainQuery, whereClauseHero: query.WhereClauseHero, whereClauseVillain: query.WhereClauseVillain,
                hero: hero, tourneyType: tourneyType, date: sinceDate, AI: isAi, size: size, effStack: es, regList: GlobalConfig.defaultRegList.ReadRegList(vs == "regs" ? true : false),
                seatActionHero: query.SeatActionHero, seatActionVillain: query.SeatActionVillain).GetCevChartParallel<StatsModel>(settings.CurrentDbRead);
        }

        /// <summary>
        /// Run a SQL OR NoSQL Query(depending on choosen DBType) that groups Hands by active player's HoleCards and returns a JSON of DataTable for a single HoleCard's Id;
        /// </summary>
        /// <param name="query">Main Query;</param>
        /// <param name="hero">Active Player;</param>
        /// <param name="tourneyType">Tournament Type;</param>
        /// <param name="sinceDate">Date, that filters only Hands played after;</param>
        /// <param name="ai">Active Player is ALL-In or not;</param>
        /// <param name="size">Size of the Bet(if any)</param>
        /// <param name="vs">Villain is / is not in the REG's list</param>
        /// <param name="es">Effective Stack(the smallest stack of all active players)</param>
        /// <param name="hcsId">HoleCard's Id (Enum CardAllSimple)</param>
        /// <param name="settings">Settings</param>
        /// <returns></returns>
        public static DataTable RequestStatsModel_Heatmap_DataGridViewByHCs(this Query query, string hero, string tourneyType, string sinceDate, string ai, string size, string vs, string es, string hcsId, Settings settings)
        {
            string isAi = settings.DbTypeRead == DataBaseType.NoSQL.GetDescription() ? ai : (ai == "0" ? "" : "^R[0-9]+ai$");
            return QueriesExtensionMethods.ConcatQueries(query.DataViewQuery, whereClauseHero: query.WhereClauseHero, whereClauseVillain: query.WhereClauseVillain,
                hero: hero, tourneyType: tourneyType, date: sinceDate, AI: isAi, size: size, effStack: es, hcsId: hcsId,
                regList: GlobalConfig.defaultRegList.ReadRegList(vs == "regs" ? true : false), seatActionHero: query.SeatActionHero, seatActionVillain: query.SeatActionVillain).GetView(settings.CurrentDbRead);
        }
    }
}
