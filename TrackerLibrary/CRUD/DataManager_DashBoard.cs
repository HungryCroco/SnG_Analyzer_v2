using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;
using TrackerLibrary.Queries.NoSQL;
using TrackerLibrary.Queries.SQL;
using TrackerLibrary.Queries;
using TrackerLibrary;
using Microsoft.VisualBasic;
using System.Numerics;
using Npgsql;

namespace TrackerLibrary.CRUD
{
    /// <summary>
    /// This class contains all the Methods needed to calculate a DashBoard UI;
    /// </summary>
    public class DataManager_DashBoard
    {
        /// <summary>
        /// Run a SQL OR NoSQL Query(depending on choosen DBType) that groups Hands by Tournament and returns a JSON of CevModel;
        /// </summary>
        /// <param name="hero">Active Player</param>
        /// <param name="tourneyType">Tournament Type</param>
        /// <param name="settings">Settings</param>
        /// <returns>List of CevModel grouped by Tournaments</returns>
        private static List<CevModel> RequestCevModel_TotalByTournament(string hero, string tourneyType, Settings settings)
        {
            return QueriesExtensionMethods.ConcatQueries(settings.DbTypeRead == DataBaseType.NoSQL.GetDescription() ? NoSQL_DashBoardQueries.sql_ExportCevPerTournamentAsJSON : SQL_DashBoardQueries.sql_ExportCevPerTournamentAsJSON, hero: hero, tourneyType: tourneyType).GetCevChartParallel<CevModel>(settings.CurrentDbRead);
        }

        /// <summary>
        /// Run a SQL OR NoSQL Query(depending on choosen DBType) that groups Hands by Date and returns a JSON of CevModel;
        /// </summary>
        /// <param name="hero">Active Player</param>
        /// <param name="tourneyType">Tournament Type</param>
        /// <param name="settings">Settings</param>
        /// <returns>List of CevModel grouped by Month</returns>
        private static List<CevModel> RequestCevModel_TotalByMonth(string hero, string tourneyType, Settings settings)
        {
            return QueriesExtensionMethods.ConcatQueries(settings.DbTypeRead == DataBaseType.NoSQL.GetDescription() ? NoSQL_DashBoardQueries.sql_ExportTlpOverviewAsJSON_CevTotal_GroupByMonths : SQL_DashBoardQueries.sql_ExportTlpOverviewAsJSON_CevTotal_GroupByMonths,
                hero: hero, tourneyType: tourneyType).GetCevChartParallel<CevModel>(settings.CurrentDbRead);
        }

        /// <summary>
        /// Run a SQL OR NoSQL Query(depending on choosen DBType) that groups Hands by Date and returns a JSON of CevModel;
        /// Query filtered By:
        ///     - 3+ players;
        ///     - Hero's position = BTN;
        ///     - Villain's position = BB;
        ///     - Villain is in the REG List;
        ///     - SB did openFold PreFlop;
        /// </summary>
        /// <param name="hero">Active Player</param>
        /// <param name="tourneyType">Tournament Type</param>
        /// <param name="settings">Settings</param>
        /// <returns>List of CevModel grouped by Month</returns>
        private static List<CevModel> RequestCevModel_3W_BTNvBBvREG(string hero, string tourneyType, Settings settings)
        {
            return QueriesExtensionMethods.ConcatQueries(settings.DbTypeRead == DataBaseType.NoSQL.GetDescription() ? NoSQL_DashBoardQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths : SQL_DashBoardQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths, 
                hero: hero, cntPlayers:3 , tourneyType: tourneyType, posHero:0 , posVillain:9, pfActors: "LIKE '09%'", regList:GlobalConfig.defaultRegList.ReadRegList(true), 
                seatActionVillain: "bbSeatActionId").GetCevChartParallel<CevModel>(settings.CurrentDbRead);
        }

        /// <summary>
        /// Run a SQL OR NoSQL Query(depending on choosen DBType) that groups Hands by Date and returns a JSON of CevModel;
        /// Query filtered By:
        ///     - 3+ players;
        ///     - Hero's position = BTN;
        ///     - Villain's position = BB;
        ///     - Villain is NOT in the REG List;
        ///     - SB did openFold PreFlop;
        /// </summary>
        /// <param name="hero">Active Player</param>
        /// <param name="tourneyType">Tournament Type</param>
        /// <param name="settings">Settings</param>
        /// <returns>List of CevModel grouped by Month</returns>
        private static List<CevModel> RequestCevModel_3W_BTNvBBvFISH(string hero, string tourneyType, Settings settings)
        {
            return QueriesExtensionMethods.ConcatQueries(settings.DbTypeRead == DataBaseType.NoSQL.GetDescription() ? NoSQL_DashBoardQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths : SQL_DashBoardQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths, 
                hero: hero, cntPlayers: 3, tourneyType: tourneyType, posHero: 0, posVillain: 9, pfActors: "LIKE '09%'", regList: GlobalConfig.defaultRegList.ReadRegList(false), 
                seatActionVillain: "bbSeatActionId").GetCevChartParallel<CevModel>(settings.CurrentDbRead);
        }

        /// <summary>
        /// Run a SQL OR NoSQL Query(depending on choosen DBType) that groups Hands by Date and returns a JSON of CevModel;
        /// Query filtered By:
        ///     - 3+ players;
        ///     - Hero's position = SB;
        ///     - Villain's position = BTN;
        ///     - Villain is in the REG List;
        ///     - BB did openFold PreFlop;
        /// </summary>
        /// <param name="hero">Active Player</param>
        /// <param name="tourneyType">Tournament Type</param>
        /// <param name="settings">Settings</param>
        /// <returns>List of CevModel grouped by Month</returns>
        private static List<CevModel> RequestCevModel_3W_SBvBTNvREG(string hero, string tourneyType, Settings settings)
        {
            return QueriesExtensionMethods.ConcatQueries(settings.DbTypeRead == DataBaseType.NoSQL.GetDescription() ? NoSQL_DashBoardQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths : SQL_DashBoardQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths, 
                hero: hero, cntPlayers: 3, tourneyType: tourneyType, posHero: 8, posVillain: 0, pfActors: "LIKE '0%'", regList: GlobalConfig.defaultRegList.ReadRegList(true), 
                seatActionVillain: "btnSeatActionId").GetCevChartParallel<CevModel>(settings.CurrentDbRead);
        }

        /// <summary>
        /// Run a SQL OR NoSQL Query(depending on choosen DBType) that groups Hands by Date and returns a JSON of CevModel;
        /// Query filtered By:
        ///     - 3+ players;
        ///     - Hero's position = SB;
        ///     - Villain's position = BTN;
        ///     - Villain is NOT in the REG List;
        ///     - BB did openFold PreFlop;
        /// </summary>
        /// <param name="hero">Active Player</param>
        /// <param name="tourneyType">Tournament Type</param>
        /// <param name="settings">Settings</param>
        /// <returns>List of CevModel grouped by Month</returns>
        private static List<CevModel> RequestCevModel_3W_SBvBTNvFISH(string hero, string tourneyType, Settings settings)
        {
            return QueriesExtensionMethods.ConcatQueries(settings.DbTypeRead == DataBaseType.NoSQL.GetDescription() ? NoSQL_DashBoardQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths : SQL_DashBoardQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths, 
                hero: hero, cntPlayers: 3, tourneyType: tourneyType, posHero: 8, posVillain: 0, pfActors: "LIKE '0%'", regList: GlobalConfig.defaultRegList.ReadRegList(false), 
                seatActionVillain: "btnSeatActionId").GetCevChartParallel<CevModel>(settings.CurrentDbRead);
        }

        /// <summary>
        /// Run a SQL OR NoSQL Query(depending on choosen DBType) that groups Hands by Date and returns a JSON of CevModel;
        /// Query filtered By:
        ///     - 3+ players;
        ///     - Hero's position = BB;
        ///     - Villain's position = BTN;
        ///     - Villain is in the REG List;
        ///     - BB did openFold PreFlop;
        /// </summary>
        /// <param name="hero">Active Player</param>
        /// <param name="tourneyType">Tournament Type</param>
        /// <param name="settings">Settings</param>
        /// <returns>List of CevModel grouped by Month</returns>
        private static List<CevModel> RequestCevModel_3W_BBvBTNvREG(string hero, string tourneyType, Settings settings)
        {
            return QueriesExtensionMethods.ConcatQueries(settings.DbTypeRead == DataBaseType.NoSQL.GetDescription() ? NoSQL_DashBoardQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths : SQL_DashBoardQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths, 
                hero: hero, cntPlayers: 3, tourneyType: tourneyType, posHero: 9, posVillain: 0, pfActors: "LIKE '09%'", regList: GlobalConfig.defaultRegList.ReadRegList(true), 
                seatActionVillain: "btnSeatActionId").GetCevChartParallel<CevModel>(settings.CurrentDbRead);
        }

        /// <summary>
        /// Run a SQL OR NoSQL Query(depending on choosen DBType) that groups Hands by Date and returns a JSON of CevModel;
        /// Query filtered By:
        ///     - 3+ players;
        ///     - Hero's position = BB;
        ///     - Villain's position = BTN;
        ///     - Villain NOT is in the REG List;
        ///     - BB did openFold PreFlop;
        /// </summary>
        /// <param name="hero">Active Player</param>
        /// <param name="tourneyType">Tournament Type</param>
        /// <param name="settings">Settings</param>
        /// <returns>List of CevModel grouped by Month</returns>
        private static List<CevModel> RequestCevModel_3W_BBvBTNvFISH(string hero, string tourneyType, Settings settings)
        {
            return QueriesExtensionMethods.ConcatQueries(settings.DbTypeRead == DataBaseType.NoSQL.GetDescription() ? NoSQL_DashBoardQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths : SQL_DashBoardQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths, 
                hero: hero, cntPlayers: 3, tourneyType: tourneyType, posHero: 9, posVillain: 0, pfActors: "LIKE '09%'", regList: GlobalConfig.defaultRegList.ReadRegList(false), 
                seatActionVillain: "btnSeatActionId").GetCevChartParallel<CevModel>(settings.CurrentDbRead);
        }

        /// <summary>
        /// Run a SQL OR NoSQL Query(depending on choosen DBType) that groups Hands by Date and returns a JSON of CevModel;
        /// Query filtered By:
        ///     - 3+ players;
        ///     - Hero's position = SB;
        ///     - Villain's position = BB;
        ///     - Villain is in the REG List;
        ///     - BTN did openFold PreFlop;
        /// </summary>
        /// <param name="hero">Active Player</param>
        /// <param name="tourneyType">Tournament Type</param>
        /// <param name="settings">Settings</param>
        /// <returns>List of CevModel grouped by Month</returns>
        private static List<CevModel> RequestCevModel_3W_SBvBBvREG(string hero, string tourneyType, Settings settings)
        {
            return QueriesExtensionMethods.ConcatQueries(settings.DbTypeRead == DataBaseType.NoSQL.GetDescription() ? NoSQL_DashBoardQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths : SQL_DashBoardQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths, 
                hero: hero, cntPlayers: 3, tourneyType: tourneyType, posHero: 8, posVillain: 9, pfActors: "NOT LIKE '0%'", regList: GlobalConfig.defaultRegList.ReadRegList(true), 
                seatActionVillain: "bbSeatActionId").GetCevChartParallel<CevModel>(settings.CurrentDbRead);
        }

        /// <summary>
        /// Run a SQL OR NoSQL Query(depending on choosen DBType) that groups Hands by Date and returns a JSON of CevModel;
        /// Query filtered By:
        ///     - 3+ players;
        ///     - Hero's position = SB;
        ///     - Villain's position = BB;
        ///     - Villain is NOT in the REG List;
        ///     - BTN did openFold PreFlop;
        /// </summary>
        /// <param name="hero">Active Player</param>
        /// <param name="tourneyType">Tournament Type</param>
        /// <param name="settings">Settings</param>
        /// <returns>List of CevModel grouped by Month</returns>
        private static List<CevModel> RequestCevModel_3W_SBvBBvFISH(string hero, string tourneyType, Settings settings)
        {
            return QueriesExtensionMethods.ConcatQueries(settings.DbTypeRead == DataBaseType.NoSQL.GetDescription() ? NoSQL_DashBoardQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths : SQL_DashBoardQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths, 
                hero: hero, cntPlayers: 3, tourneyType: tourneyType, posHero: 8, posVillain: 9, pfActors: "NOT LIKE '0%'", regList: GlobalConfig.defaultRegList.ReadRegList(false), 
                seatActionVillain: "bbSeatActionId").GetCevChartParallel<CevModel>(settings.CurrentDbRead);
        }

        /// <summary>
        /// Run a SQL OR NoSQL Query(depending on choosen DBType) that groups Hands by Date and returns a JSON of CevModel;
        /// Query filtered By:
        ///     - 3+ players;
        ///     - Hero's position = BB;
        ///     - Villain's position = SB;
        ///     - Villain is in the REG List;
        ///     - BTN did openFold PreFlop;
        /// </summary>
        /// <param name="hero">Active Player</param>
        /// <param name="tourneyType">Tournament Type</param>
        /// <param name="settings">Settings</param>
        /// <returns>List of CevModel grouped by Month</returns>
        private static List<CevModel> RequestCevModel_3W_BBvSBvREG(string hero, string tourneyType, Settings settings)
        {
            return QueriesExtensionMethods.ConcatQueries(settings.DbTypeRead == DataBaseType.NoSQL.GetDescription() ? NoSQL_DashBoardQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths : SQL_DashBoardQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths, 
                hero: hero, cntPlayers: 3, tourneyType: tourneyType, posHero: 9, posVillain: 8, pfActors: "NOT LIKE '0%'", regList: GlobalConfig.defaultRegList.ReadRegList(true), 
                seatActionVillain: "sbSeatActionId").GetCevChartParallel<CevModel>(settings.CurrentDbRead);
        }

        /// <summary>
        /// Run a SQL OR NoSQL Query(depending on choosen DBType) that groups Hands by Date and returns a JSON of CevModel;
        /// Query filtered By:
        ///     - 3+ players;
        ///     - Hero's position = BB;
        ///     - Villain's position = SB;
        ///     - Villain is NOT in the REG List;
        ///     - BTN did openFold PreFlop;
        /// </summary>
        /// <param name="hero">Active Player</param>
        /// <param name="tourneyType">Tournament Type</param>
        /// <param name="settings">Settings</param>
        /// <returns>List of CevModel grouped by Month</returns>
        private static List<CevModel> RequestCevModel_3W_BBvSBvFISH(string hero, string tourneyType, Settings settings)
        {
            return QueriesExtensionMethods.ConcatQueries(settings.DbTypeRead == DataBaseType.NoSQL.GetDescription() ? NoSQL_DashBoardQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths : SQL_DashBoardQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths, 
                hero: hero, cntPlayers: 3, tourneyType: tourneyType, posHero: 9, posVillain: 8, pfActors: "NOT LIKE '0%'", regList: GlobalConfig.defaultRegList.ReadRegList(false), 
                seatActionVillain: "sbSeatActionId").GetCevChartParallel<CevModel>(settings.CurrentDbRead);
        }

        /// <summary>
        /// Run a SQL OR NoSQL Query(depending on choosen DBType) that groups Hands by Date and returns a JSON of CevModel;
        /// Query filtered By:
        ///     - 2 players;
        ///     - Hero's position = SB;
        ///     - Villain's position = BB;
        ///     - Villain is in the REG List;
        /// </summary>
        /// <param name="hero">Active Player</param>
        /// <param name="tourneyType">Tournament Type</param>
        /// <param name="settings">Settings</param>
        /// <returns>List of CevModel grouped by Month</returns>
        private static List<CevModel> RequestCevModel_HU_SBvREG(string hero, string tourneyType, Settings settings)
        {
            return QueriesExtensionMethods.ConcatQueries(settings.DbTypeRead == DataBaseType.NoSQL.GetDescription() ? NoSQL_DashBoardQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths : SQL_DashBoardQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths, 
                hero: hero, cntPlayers: 2, tourneyType: tourneyType, posHero: 8, posVillain: 9, pfActors: "LIKE '%'", regList: GlobalConfig.defaultRegList.ReadRegList(true), 
                seatActionVillain: "bbSeatActionId").GetCevChartParallel<CevModel>(settings.CurrentDbRead);
        }

        /// <summary>
        /// Run a SQL OR NoSQL Query(depending on choosen DBType) that groups Hands by Date and returns a JSON of CevModel;
        /// Query filtered By:
        ///     - 2 players;
        ///     - Hero's position = SB;
        ///     - Villain's position = BB;
        ///     - Villain is NOT in the REG List;
        /// </summary>
        /// <param name="hero">Active Player</param>
        /// <param name="tourneyType">Tournament Type</param>
        /// <param name="settings">Settings</param>
        /// <returns>List of CevModel grouped by Month</returns>
        private static List<CevModel> RequestCevModel_HU_SBvFISH(string hero, string tourneyType, Settings settings)
        {
            return QueriesExtensionMethods.ConcatQueries(settings.DbTypeRead == DataBaseType.NoSQL.GetDescription() ? NoSQL_DashBoardQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths : SQL_DashBoardQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths, 
                hero: hero, cntPlayers: 2, tourneyType: tourneyType, posHero: 8, posVillain: 9, pfActors: "LIKE '%'", regList: GlobalConfig.defaultRegList.ReadRegList(false), 
                seatActionVillain: "bbSeatActionId").GetCevChartParallel<CevModel>(settings.CurrentDbRead);
        }

        /// <summary>
        /// Run a SQL OR NoSQL Query(depending on choosen DBType) that groups Hands by Date and returns a JSON of CevModel;
        /// Query filtered By:
        ///     - 2 players;
        ///     - Hero's position = BB;
        ///     - Villain's position = SB;
        ///     - Villain is in the REG List;
        /// </summary>
        /// <param name="hero">Active Player</param>
        /// <param name="tourneyType">Tournament Type</param>
        /// <param name="settings">Settings</param>
        /// <returns>List of CevModel grouped by Month</returns>
        private static List<CevModel> RequestCevModel_HU_BBvREG(string hero, string tourneyType, Settings settings)
        {
            return QueriesExtensionMethods.ConcatQueries(settings.DbTypeRead == DataBaseType.NoSQL.GetDescription() ? NoSQL_DashBoardQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths : SQL_DashBoardQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths, 
                hero: hero, cntPlayers: 2, tourneyType: tourneyType, posHero: 9, posVillain: 8, pfActors: "LIKE '%'", regList: GlobalConfig.defaultRegList.ReadRegList(true), 
                seatActionVillain: "sbSeatActionId").GetCevChartParallel<CevModel>(settings.CurrentDbRead);
        }

        /// <summary>
        /// Run a SQL OR NoSQL Query(depending on choosen DBType) that groups Hands by Date and returns a JSON of CevModel;
        /// Query filtered By:
        ///     - 2 players;
        ///     - Hero's position = BB;
        ///     - Villain's position = SB;
        ///     - Villain is NOT in the REG List;
        /// </summary>
        /// <param name="hero">Active Player</param>
        /// <param name="tourneyType">Tournament Type</param>
        /// <param name="settings">Settings</param>
        /// <returns>List of CevModel grouped by Month</returns>
        private static List<CevModel> RequestCevModel_HU_BBvFISH(string hero, string tourneyType, Settings settings)
        {
            return QueriesExtensionMethods.ConcatQueries(settings.DbTypeRead == DataBaseType.NoSQL.GetDescription() ? NoSQL_DashBoardQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths : SQL_DashBoardQueries.sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths, 
                hero: hero, cntPlayers: 2, tourneyType: tourneyType, posHero: 9, posVillain: 8, pfActors: "LIKE '%'", regList: GlobalConfig.defaultRegList.ReadRegList(false), 
                seatActionVillain: "sbSeatActionId").GetCevChartParallel<CevModel>(settings.CurrentDbRead);
        }

        /// <summary>
        /// 1. Checks if a Dashboard with the given parameters is already available in the DB;
        /// 2. [optional] If not available, runs all necessary queries to calculate a DashboardModel;
        /// 3. [optional] Saves the DashBoardModel to DB;
        /// Depending on Settings-DBTypeRead runs SQL Or NoSQL Queries;
        /// </summary>
        /// <param name="hero">Active Player</param>
        /// <param name="tourneyType">Tournament Type</param>
        /// <param name="settings">Settings</param>
        /// <returns>A DashBoardModel containing all the Info needed to run a DashBoard UI;</returns>
        public static DashBoardModel RequestDashBoard(string hero, string tourneyType, Settings settings)
        {
            //Request DashBoardModel from the DB;
            var dashBoardModel = NoSQL_Connector.GetDashBoardModel(hero, tourneyType, settings.CurrentDbRead);
            
            //Checks if the DashBoardModel is available( not NULL);
            if (dashBoardModel.CevModel_Total_ByTournament != null)
            {
                return dashBoardModel;
            }
            else
            {
                //Runs all Queries;
                Task t1 = Task.Run(() => { dashBoardModel.CevModel_Total_ByTournament = RequestCevModel_TotalByTournament(hero, tourneyType, settings); });
                Task t2 = Task.Run(() => { dashBoardModel.CevModel_Total_ByMonth = RequestCevModel_TotalByMonth(hero, tourneyType, settings); });

                Task t3 = Task.Run(() => { dashBoardModel.CevModel_3W_BTNvBBvREG = RequestCevModel_3W_BTNvBBvREG(hero, tourneyType, settings); });
                Task t4 = Task.Run(() => { dashBoardModel.CevModel_3W_BTNvBBvFISH = RequestCevModel_3W_BTNvBBvFISH(hero, tourneyType, settings); });
                Task t5 = Task.Run(() => { dashBoardModel.CevModel_3W_SBvBTNvREG = RequestCevModel_3W_SBvBTNvREG(hero, tourneyType, settings); });
                Task t6 = Task.Run(() => { dashBoardModel.CevModel_3W_SBvBTNvFISH = RequestCevModel_3W_SBvBTNvFISH(hero, tourneyType, settings); });
                Task t7 = Task.Run(() => { dashBoardModel.CevModel_3W_BBvBTNvREG = RequestCevModel_3W_BBvBTNvREG(hero, tourneyType, settings); });
                Task t8 = Task.Run(() => { dashBoardModel.CevModel_3W_BBvBTNvFISH = RequestCevModel_3W_BBvBTNvFISH(hero, tourneyType, settings); });

                Task t9 = Task.Run(() => { dashBoardModel.CevModel_3W_SBvBBvREG = RequestCevModel_3W_SBvBBvREG(hero, tourneyType, settings); });
                Task t10 = Task.Run(() => { dashBoardModel.CevModel_3W_SBvBBvFISH = RequestCevModel_3W_SBvBBvFISH(hero, tourneyType, settings); });
                Task t11 = Task.Run(() => { dashBoardModel.CevModel_3W_BBvSBvREG = RequestCevModel_3W_BBvSBvREG(hero, tourneyType, settings); });
                Task t12 = Task.Run(() => { dashBoardModel.CevModel_3W_BBvSBvFISH = RequestCevModel_3W_BBvSBvFISH(hero, tourneyType, settings); });

                Task t13 = Task.Run(() => { dashBoardModel.CevModel_HU_SBvREG = RequestCevModel_HU_SBvREG(hero, tourneyType, settings); });
                Task t14 = Task.Run(() => { dashBoardModel.CevModel_HU_SBvFISH = RequestCevModel_HU_SBvFISH(hero, tourneyType, settings); });
                Task t15 = Task.Run(() => { dashBoardModel.CevModel_HU_BBvREG = RequestCevModel_HU_BBvREG(hero, tourneyType, settings); });
                Task t16 = Task.Run(() => { dashBoardModel.CevModel_HU_BBvFISH = RequestCevModel_HU_BBvFISH(hero, tourneyType, settings); });

                t1.Wait();
                t2.Wait();
                t3.Wait();
                t4.Wait();
                t5.Wait();
                t6.Wait();
                t7.Wait();
                t8.Wait();
                t9.Wait();
                t10.Wait();
                t11.Wait();
                t12.Wait();
                t13.Wait();
                t14.Wait();
                t15.Wait();
                t16.Wait();

                //Saves the calculated DashBoardModel to DB;
                NoSQL_Connector.InsertDashBoardModelToNoSqlDb(settings.CurrentDbRead, GlobalConfig.dashboardTableName, hero, tourneyType, dashBoardModel);



                return dashBoardModel;
            }
        }
    }
}
