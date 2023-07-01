using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;
using TrackerLibrary.Queries.NoSQL;
using TrackerLibrary.Queries;
using Microsoft.VisualBasic;
using System.Numerics;
using Npgsql;

namespace TrackerLibrary.CRUD
{
    public class NoSQL_CalculateHeatMap
    {

        public static List<StatsModel> RequestStatsModel_BvbIso(string hero, string tourneyType, string sinceDate, string ai, string size, string vs)
        {
            return QueriesExtensionMethods.ConcatQueries(HeatMapQueries.sql_ExportHeatMapAsJSON_BvB_Iso, hero: hero, tourneyType: tourneyType, date: sinceDate, AI: ai, size: size, regList: GlobalConfig.regList.ReadRegList(vs == "REGs" ? true : false)).GetCevChartParallel<StatsModel>();
        }

        
    }
}
