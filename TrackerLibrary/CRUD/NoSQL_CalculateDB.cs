using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;
using TrackerLibrary.Queries.NoSQL;
using TrackerLibrary.Queries;

namespace TrackerLibrary.CRUD
{
    public class NoSQL_CalculateDB
    {

        public static List<CevModel> RequestCevModel_Total(string hero, string tourneyType)
        {
            return QueriesExtensionMethods.ConcatQueries(cevRequestQueries.sql_ExportCevPerTournamentAsJSON, hero: hero, tourneyType: tourneyType).GetCevChartParallel();
        }

        public static List<CevModel> RequestCevModel_3W_BTNvBBvREG(string hero, string tourneyType)
        {
            return QueriesExtensionMethods.ConcatQueries(cevRequestQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths, hero: hero, tourneyType: tourneyType, posHero:0 , posVillain:8, regList:GlobalConfig.regList.ReadRegList(true)).GetCevChartParallel();
        }

        public static List<CevModel> RequestCevModel_3W_SBvBBvREG(string hero, string tourneyType)
        {
            return QueriesExtensionMethods.ConcatQueries(cevRequestQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths, hero: hero, tourneyType: tourneyType, posHero: 9, posVillain: 8, regList: GlobalConfig.regList.ReadRegList(true)).GetCevChartParallel();
        }




    }
}
