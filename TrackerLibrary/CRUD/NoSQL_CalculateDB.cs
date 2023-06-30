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

namespace TrackerLibrary.CRUD
{
    public class NoSQL_CalculateDB
    {

        private static List<CevModel> RequestCevModel_TotalByTournament(string hero, string tourneyType)
        {
            return QueriesExtensionMethods.ConcatQueries(cevRequestQueries.sql_ExportCevPerTournamentAsJSON, hero: hero, tourneyType: tourneyType).GetCevChartParallel();
        }

        private static List<CevModel> RequestCevModel_TotalByMonth(string hero, string tourneyType)
        {
            return QueriesExtensionMethods.ConcatQueries(cevRequestQueries.sql_ExportTlpOverviewAsJSON_CevTotal_GroupByMonths, hero: hero, tourneyType: tourneyType).GetCevChartParallel();
        }

        private static List<CevModel> RequestCevModel_3W_BTNvBBvREG(string hero, string tourneyType)
        {
            return QueriesExtensionMethods.ConcatQueries(cevRequestQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths,whereClause: "AND ha->'Info'->> 'pf_actors' LIKE '09%'", hero: hero, cntPlayers:3 , tourneyType: tourneyType, posHero:0 , posVillain:8, regList:GlobalConfig.regList.ReadRegList(true)).GetCevChartParallel();
        }

        private static List<CevModel> RequestCevModel_3W_BTNvBBvFISH(string hero, string tourneyType)
        {
            return QueriesExtensionMethods.ConcatQueries(cevRequestQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths, whereClause: "AND ha->'Info'->> 'pf_actors' LIKE '09%'", hero: hero, cntPlayers: 3, tourneyType: tourneyType, posHero: 0, posVillain: 8, regList: GlobalConfig.regList.ReadRegList(false)).GetCevChartParallel();
        }

        private static List<CevModel> RequestCevModel_3W_SBvBTNvREG(string hero, string tourneyType)
        {
            return QueriesExtensionMethods.ConcatQueries(cevRequestQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths, whereClause: "AND ha->'Info'->> 'pf_actors' LIKE '0%'", hero: hero, cntPlayers: 3, tourneyType: tourneyType, posHero: 8, posVillain: 0, regList: GlobalConfig.regList.ReadRegList(true)).GetCevChartParallel();
        }

        private static List<CevModel> RequestCevModel_3W_SBvBTNvFISH(string hero, string tourneyType)
        {
            return QueriesExtensionMethods.ConcatQueries(cevRequestQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths, whereClause: "AND ha->'Info'->> 'pf_actors' LIKE '0%'", hero: hero, cntPlayers: 3, tourneyType: tourneyType, posHero: 8, posVillain: 0, regList: GlobalConfig.regList.ReadRegList(false)).GetCevChartParallel();
        }

        private static List<CevModel> RequestCevModel_3W_BBvBTNvREG(string hero, string tourneyType)
        {
            return QueriesExtensionMethods.ConcatQueries(cevRequestQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths, whereClause: "AND ha->'Info'->> 'pf_actors' LIKE '09%'", hero: hero, cntPlayers: 3, tourneyType: tourneyType, posHero: 9, posVillain: 0, regList: GlobalConfig.regList.ReadRegList(true)).GetCevChartParallel();
        }

        private static List<CevModel> RequestCevModel_3W_BBvBTNvFISH(string hero, string tourneyType)
        {
            return QueriesExtensionMethods.ConcatQueries(cevRequestQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths, whereClause: "AND ha->'Info'->> 'pf_actors' LIKE '09%'", hero: hero, cntPlayers: 3, tourneyType: tourneyType, posHero: 9, posVillain: 0, regList: GlobalConfig.regList.ReadRegList(false)).GetCevChartParallel();
        }

        private static List<CevModel> RequestCevModel_3W_SBvBBvREG(string hero, string tourneyType)
        {
            return QueriesExtensionMethods.ConcatQueries(cevRequestQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths, whereClause: "AND ha->'Info'->> 'pf_actors' NOT LIKE '0%'", hero: hero, cntPlayers: 3, tourneyType: tourneyType, posHero: 8, posVillain: 9, regList: GlobalConfig.regList.ReadRegList(true)).GetCevChartParallel();
        }

        private static List<CevModel> RequestCevModel_3W_SBvBBvFISH(string hero, string tourneyType)
        {
            return QueriesExtensionMethods.ConcatQueries(cevRequestQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths, whereClause: "AND ha->'Info'->> 'pf_actors' NOT LIKE '0%'", hero: hero, cntPlayers: 3, tourneyType: tourneyType, posHero: 8, posVillain: 9, regList: GlobalConfig.regList.ReadRegList(false)).GetCevChartParallel();
        }

        private static List<CevModel> RequestCevModel_3W_BBvSBvREG(string hero, string tourneyType)
        {
            return QueriesExtensionMethods.ConcatQueries(cevRequestQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths, whereClause: "AND ha->'Info'->> 'pf_actors' NOT LIKE '0%'", hero: hero, cntPlayers: 3, tourneyType: tourneyType, posHero: 9, posVillain: 8, regList: GlobalConfig.regList.ReadRegList(true)).GetCevChartParallel();
        }

        private static List<CevModel> RequestCevModel_3W_BBvSBvFISH(string hero, string tourneyType)
        {
            return QueriesExtensionMethods.ConcatQueries(cevRequestQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths, whereClause: "AND ha->'Info'->> 'pf_actors' NOT LIKE '0%'", hero: hero, cntPlayers: 3, tourneyType: tourneyType, posHero: 9, posVillain: 8, regList: GlobalConfig.regList.ReadRegList(false)).GetCevChartParallel();
        }

        private static List<CevModel> RequestCevModel_HU_SBvREG(string hero, string tourneyType)
        {
            return QueriesExtensionMethods.ConcatQueries(cevRequestQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths, hero: hero, cntPlayers: 2, tourneyType: tourneyType, posHero: 8, posVillain: 9, regList: GlobalConfig.regList.ReadRegList(true)).GetCevChartParallel();
        }

        private static List<CevModel> RequestCevModel_HU_SBvFISH(string hero, string tourneyType)
        {
            return QueriesExtensionMethods.ConcatQueries(cevRequestQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths, hero: hero, cntPlayers: 2, tourneyType: tourneyType, posHero: 8, posVillain: 9, regList: GlobalConfig.regList.ReadRegList(false)).GetCevChartParallel();
        }

        private static List<CevModel> RequestCevModel_HU_BBvREG(string hero, string tourneyType)
        {
            return QueriesExtensionMethods.ConcatQueries(cevRequestQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths, hero: hero, cntPlayers: 2, tourneyType: tourneyType, posHero: 8, posVillain: 9, regList: GlobalConfig.regList.ReadRegList(true)).GetCevChartParallel();
        }

        private static List<CevModel> RequestCevModel_HU_BBvFISH(string hero, string tourneyType)
        {
            return QueriesExtensionMethods.ConcatQueries(cevRequestQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths, hero: hero, cntPlayers: 2, tourneyType: tourneyType, posHero: 8, posVillain: 9, regList: GlobalConfig.regList.ReadRegList(false)).GetCevChartParallel();
        }

        public static DashBoardModel RequestDashBoard(string hero, string tourneyType)
        {
            var dashBoardModel = NoSQL_Connector.GetDashBoardModel(hero, tourneyType);

            if (dashBoardModel.CevModel_Total_ByTournament != null)
            {
                return dashBoardModel;
            }
            else
            {
                //DashBoardModel dashBoardModel = new DashBoardModel();
                dashBoardModel.CevModel_Total_ByTournament = RequestCevModel_TotalByTournament(hero, tourneyType);
                dashBoardModel.CevModel_Total_ByMonth = RequestCevModel_TotalByMonth(hero, tourneyType);

                dashBoardModel.CevModel_3W_BTNvBBvREG = RequestCevModel_3W_BTNvBBvREG(hero, tourneyType);
                dashBoardModel.CevModel_3W_BTNvBBvFISH = RequestCevModel_3W_BTNvBBvFISH(hero, tourneyType);
                dashBoardModel.CevModel_3W_SBvBTNvREG = RequestCevModel_3W_SBvBTNvREG(hero, tourneyType);
                dashBoardModel.CevModel_3W_SBvBTNvFISH = RequestCevModel_3W_SBvBTNvFISH(hero, tourneyType);
                dashBoardModel.CevModel_3W_BBvBTNvREG = RequestCevModel_3W_BBvBTNvREG(hero, tourneyType);
                dashBoardModel.CevModel_3W_BBvBTNvFISH = RequestCevModel_3W_BBvBTNvFISH(hero, tourneyType);

                dashBoardModel.CevModel_3W_SBvBBvREG = RequestCevModel_3W_SBvBBvREG(hero, tourneyType);
                dashBoardModel.CevModel_3W_SBvBBvFISH = RequestCevModel_3W_SBvBBvFISH(hero, tourneyType);
                dashBoardModel.CevModel_3W_BBvSBvREG = RequestCevModel_3W_BBvSBvREG(hero, tourneyType);
                dashBoardModel.CevModel_3W_BBvSBvFISH = RequestCevModel_3W_BBvSBvFISH(hero, tourneyType);

                dashBoardModel.CevModel_HU_SBvREG = RequestCevModel_HU_SBvREG(hero, tourneyType);
                dashBoardModel.CevModel_HU_SBvFISH = RequestCevModel_HU_SBvFISH(hero, tourneyType);
                dashBoardModel.CevModel_HU_BBvREG = RequestCevModel_HU_BBvREG(hero, tourneyType);
                dashBoardModel.CevModel_HU_BBvFISH = RequestCevModel_HU_BBvFISH(hero, tourneyType);

                NoSQL_Connector.InsertDashBoardModelToNoSqlDb(GlobalConfig.dbName, "dashboard", hero, tourneyType, dashBoardModel);

                return dashBoardModel;
            }     
        }
    }
}
