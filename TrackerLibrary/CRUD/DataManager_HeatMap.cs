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
using static TrackerLibrary.Models.SettingsModel;

namespace TrackerLibrary.CRUD
{
    public static class DataManager_HeatMap
    {

        public static List<StatsModel> RequestStatsModel_Heatmap(this Query query, string hero, string tourneyType, string sinceDate, string ai, string size, string vs, string es, SettingsModel.Settings settings)
        {
            string isAi = settings.DbTypeRead == DataBaseType.NoSQL.GetDescription() ? ai : (ai == "0" ? "" : "^R[0-9]+ai$");
            return QueriesExtensionMethods.ConcatQueries(query.MainQuery, whereClauseHero:query.WhereClauseHero, whereClauseVillain:query.WhereClauseVillain, 
                hero: hero, tourneyType: tourneyType, date: sinceDate, AI: isAi, size: size, effStack: es,  regList: GlobalConfig.defaultRegList.ReadRegList(vs == "regs" ? true : false), 
                seatActionHero: query.SeatActionHero, seatActionVillain: query.SeatActionVillain).GetCevChartParallel<StatsModel>(settings.CurrentDbRead);
        }

        public static DataTable RequestStatsModel_Heatmap_DataGridViewByHCs(this Query query, string hero, string tourneyType, string sinceDate, string ai, string size, string vs, string es, string hcsId, SettingsModel.Settings settings)
        {
            string isAi = settings.DbTypeRead == DataBaseType.NoSQL.GetDescription() ? ai : (ai == "0" ? "" : "^R[0-9]+ai$");
            return QueriesExtensionMethods.ConcatQueries(query.DataViewQuery, whereClauseHero: query.WhereClauseHero, whereClauseVillain: query.WhereClauseVillain, 
                hero: hero, tourneyType: tourneyType, date: sinceDate, AI: isAi, size: size, effStack: es, hcsId: hcsId, 
                regList: GlobalConfig.defaultRegList.ReadRegList(vs == "regs" ? true : false), seatActionHero: query.SeatActionHero, seatActionVillain: query.SeatActionVillain).GetView(settings.CurrentDbRead);
        }

    }
}
