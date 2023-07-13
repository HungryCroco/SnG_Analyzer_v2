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
using System.Data;

namespace TrackerLibrary.CRUD
{
    public static class NoSQL_CalculateHeatMap
    {

        public static List<StatsModel> RequestStatsModel_Heatmap(this QueryModel.Query query, string hero, string tourneyType, string sinceDate, string ai, string size, string vs, string es)
        {
            string isAi = GlobalConfig.dbType == DataBaseType.NoSQL ? ai : (ai == "0" ? "" : "^R[0-9]+ai$");
            return QueriesExtensionMethods.ConcatQueries(query.MainQuery, whereClauseHero:query.WhereClauseHero, whereClauseVillain:query.WhereClauseVillain, 
                hero: hero, tourneyType: tourneyType, date: sinceDate, AI: isAi, size: size, effStack: es,  regList: GlobalConfig.defaultRegList.ReadRegList(vs == "regs" ? true : false), 
                seatActionHero: query.SeatActionHero, seatActionVillain: query.SeatActionVillain).GetCevChartParallel<StatsModel>();
        }

        public static DataTable RequestStatsModel_Heatmap_DataGridViewByHCs(this QueryModel.Query query, string hero, string tourneyType, string sinceDate, string ai, string size, string vs, string es, string hcsId)
        {
            string isAi = GlobalConfig.dbType == DataBaseType.NoSQL ? ai : (ai == "0" ? "" : "^R[0-9]+ai$");
            return QueriesExtensionMethods.ConcatQueries(query.DataViewQuery, whereClauseHero: query.WhereClauseHero, whereClauseVillain: query.WhereClauseVillain, 
                hero: hero, tourneyType: tourneyType, date: sinceDate, AI: isAi, size: size, effStack: es, hcsId: hcsId, 
                regList: GlobalConfig.defaultRegList.ReadRegList(vs == "regs" ? true : false), seatActionHero: query.SeatActionHero, seatActionVillain: query.SeatActionVillain).GetView();
        }

    }
}
