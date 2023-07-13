using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Queries.SQL;
using TrackerLibrary.Models;
using System.Xml.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Data;
using System.Buffers;

namespace TrackerLibrary.CRUD
{
    public static class SQL_Connector
    {
        public static void ImportHandsToSqlDb(string dbName, List<Hand> hands)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            CreateDatabase(dbName);

            NpgsqlConnection conn = new NpgsqlConnection(GlobalConfig.GetConnectionString(dbName));
            conn.Open();

            foreach (var hand in hands)
            {
                ExportEntireHand(hand, ref dbName, ref conn);
            }


            conn.Close();

            Console.WriteLine("-----");
            Console.WriteLine("Import Time: " + watch.ElapsedMilliseconds / 1000.0 + "s");
        }

        public static void CreateDatabase(string dbName)
        {
            NpgsqlConnection conn = new();

            try
            {
                conn = new NpgsqlConnection(GlobalConfig.GetConnectionString());
                var cmd_createDB = new NpgsqlCommand($"CREATE DATABASE {dbName}; ", conn);


                conn.Open();
                cmd_createDB.ExecuteNonQuery();
                conn.Close();

                conn = new NpgsqlConnection(GlobalConfig.GetConnectionString(dbName));

                var cmd_CreateTable_HandAsString = new NpgsqlCommand(SQL_CreateDatabaseQueries.sql_CreateTable_HandAsString, conn);
                var cmd_CreateTable_HoleCardsSimple = new NpgsqlCommand(SQL_CreateDatabaseQueries.sql_CreateTable_HoleCardsSimple, conn);
                var cmd_CreateTable_Card = new NpgsqlCommand(SQL_CreateDatabaseQueries.sql_CreateTable_Card, conn);
                var cmd_CreateTable_Room = new NpgsqlCommand(SQL_CreateDatabaseQueries.sql_CreateTable_Room, conn);
                var cmd_CreateTable_Player = new NpgsqlCommand(SQL_CreateDatabaseQueries.sql_CreateTable_Player, conn);
                var cmd_CreateTable_Tournament = new NpgsqlCommand(SQL_CreateDatabaseQueries.sql_CreateTable_Tournament, conn);
                var cmd_CreateTable_SeatAction = new NpgsqlCommand(SQL_CreateDatabaseQueries.sql_CreateTable_SeatAction, conn);
                var cmd_CreateTable_Hands = new NpgsqlCommand(SQL_CreateDatabaseQueries.sql_CreateTable_Hands, conn);

                conn.Open();

                cmd_CreateTable_HandAsString.ExecuteNonQuery();
                cmd_CreateTable_HoleCardsSimple.ExecuteNonQuery();
                cmd_CreateTable_Card.ExecuteNonQuery();
                cmd_CreateTable_Room.ExecuteNonQuery();
                cmd_CreateTable_Player.ExecuteNonQuery();
                cmd_CreateTable_Tournament.ExecuteNonQuery();
                cmd_CreateTable_SeatAction.ExecuteNonQuery();
                cmd_CreateTable_Hands.ExecuteNonQuery();

                ImportToHoleCardsSimple(ref conn);
                ImportToCard(ref conn);
            }
            catch (Exception)
            {
            }
            conn.Close();
        }

        public static void ExportEntireHand(Hand hand, ref string dbName, ref NpgsqlConnection conn)
        {

            SQL_ImportIds currImportIds = new();

            ImportToRoom(ref hand, ref currImportIds, ref conn);
            ImportToPlayer(ref hand, ref currImportIds, ref conn);
            ImportToTournament(ref hand, ref currImportIds, ref conn);

            ImportToHandAsString(ref hand, ref currImportIds, ref conn);

            ImportToHand(ref hand, ref currImportIds, ref conn);




        }

        private static int ImportToRoom(ref Hand hand, ref SQL_ImportIds currHandImportIds, ref NpgsqlConnection conn)
        {
            var cmd = new NpgsqlCommand(SQL_ImportQueries.sql_ImportRoom, conn);


            cmd.Parameters.Clear();
            cmd.Parameters.Add(new NpgsqlParameter("room", hand.Info.Room));

            var output = cmd.ExecuteScalar();
            int outputId = int.Parse(output.ToString());
            currHandImportIds.RoomId = outputId;


            return outputId;
        }

        private static int ImportToPlayer(ref Hand hand, ref SQL_ImportIds currHandImportIds, ref NpgsqlConnection conn)
        {
            int heroId = 0;

            foreach (var playerNickName in hand.Info.Players)
            {
                var cmd = new NpgsqlCommand(SQL_ImportQueries.sql_ImportPlayer, conn);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new NpgsqlParameter("activeplayer", playerNickName));
                cmd.Parameters.Add(new NpgsqlParameter("roomId", currHandImportIds.RoomId));

                var output = cmd.ExecuteScalar();
                int outputId = int.Parse(output.ToString());

                int currSeatNum = hand.SeatActions[playerNickName].SeatNumber;
                currHandImportIds.PlayerIds[currSeatNum] = outputId;

                if (playerNickName == hand.Info.Hero)
                {
                    heroId = outputId;
                    currHandImportIds.PlayerId_Hero = outputId;
                }
            }

            return heroId;
        }

        private static int ImportToTournament(ref Hand hand, ref SQL_ImportIds currHandImportIds, ref NpgsqlConnection conn)
        {

            var cmd = new NpgsqlCommand(SQL_ImportQueries.sql_ImportTournament, conn);

            cmd.Parameters.Clear();
            cmd.Parameters.Add(new NpgsqlParameter("roomId", currHandImportIds.RoomId));
            cmd.Parameters.Add(new NpgsqlParameter("tournamentIdBySite", hand.Info.TournamentIdBySite));
            cmd.Parameters.Add(new NpgsqlParameter("tournamenttype", hand.Info.TournamentType));
            cmd.Parameters.Add(new NpgsqlParameter("tournamentdate", hand.Info.Date));
            cmd.Parameters.Add(new NpgsqlParameter("currency", hand.Info.Currency));
            cmd.Parameters.Add(new NpgsqlParameter("amt_buyin", hand.Info.BuyIn));
            cmd.Parameters.Add(new NpgsqlParameter("amt_fee", hand.Info.Fee));
            cmd.Parameters.Add(new NpgsqlParameter("amt_prize_pool_real", DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("cnt_players", Convert.ToInt32(hand.Info.CntPlayers)));
            cmd.Parameters.Add(new NpgsqlParameter("seat1playerId", (currHandImportIds.PlayerIds[1] != 0) ? Convert.ToInt32(currHandImportIds.PlayerIds[1]) : DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("seat2playerId", (currHandImportIds.PlayerIds[2] != 0) ? Convert.ToInt32(currHandImportIds.PlayerIds[2]) : DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("seat3playerId", (currHandImportIds.PlayerIds[3] != 0) ? Convert.ToInt32(currHandImportIds.PlayerIds[3]) : DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("seat4playerId", (currHandImportIds.PlayerIds[4] != 0) ? Convert.ToInt32(currHandImportIds.PlayerIds[4]) : DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("seat5playerId", (currHandImportIds.PlayerIds[5] != 0) ? Convert.ToInt32(currHandImportIds.PlayerIds[5]) : DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("seat6playerId", (currHandImportIds.PlayerIds[6] != 0) ? Convert.ToInt32(currHandImportIds.PlayerIds[6]) : DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("seat7playerId", (currHandImportIds.PlayerIds[7] != 0) ? Convert.ToInt32(currHandImportIds.PlayerIds[7]) : DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("seat8playerId", (currHandImportIds.PlayerIds[8] != 0) ? Convert.ToInt32(currHandImportIds.PlayerIds[8]) : DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("seat9playerId", (currHandImportIds.PlayerIds[9] != 0) ? Convert.ToInt32(currHandImportIds.PlayerIds[9]) : DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("seat10playerId", (currHandImportIds.PlayerIds[10] != 0) ? Convert.ToInt32(currHandImportIds.PlayerIds[10]) : DBNull.Value));

            var output = cmd.ExecuteScalar();

            int outputId = int.Parse(output.ToString());
            currHandImportIds.TournamentId = outputId;

            return outputId;
        }

        private static int ImportToHandAsString(ref Hand hand, ref SQL_ImportIds importIds, ref NpgsqlConnection conn)
        {
            var cmd = new NpgsqlCommand(SQL_ImportQueries.sql_ImportHandAsAtring, conn);

            cmd.Parameters.Clear();
            cmd.Parameters.Add(new NpgsqlParameter("fullHand", hand.Info.FullHandAsString));

            var output = cmd.ExecuteScalar();

            int outputId = int.Parse(output.ToString());
            importIds.HandId = outputId;

            return outputId;
        }

        public static void ImportToSeatAction(SeatAction seatAction, ref SQL_ImportIds importIds, ref NpgsqlConnection conn)
        {
            var cmd = new NpgsqlCommand(SQL_ImportQueries.sql_ImportSeatAction, conn);

            cmd.Parameters.Clear();
            cmd.Parameters.Add(new NpgsqlParameter("PlayerId", importIds.PlayerIds[seatAction.SeatNumber]));
            cmd.Parameters.Add(new NpgsqlParameter("SeatNumber", Convert.ToInt32(seatAction.SeatNumber)));
            cmd.Parameters.Add(new NpgsqlParameter("Position", Convert.ToInt32(seatAction.SeatPosition)));
            cmd.Parameters.Add(new NpgsqlParameter("StartingStack", seatAction.StartingStack));
            cmd.Parameters.Add(new NpgsqlParameter("PreflopAction", seatAction.Actions.PreFlop.ToString()));
            cmd.Parameters.Add(new NpgsqlParameter("FlopAction", seatAction.Actions.Flop.ToString()));
            cmd.Parameters.Add(new NpgsqlParameter("TurnAction", seatAction.Actions.Turn.ToString()));
            cmd.Parameters.Add(new NpgsqlParameter("RiverAction", seatAction.Actions.River.ToString()));
            //cmd.Parameters.Add(new NpgsqlParameter("chips_won", seatAction.ChipsWon));
            //cmd.Parameters.Add(new NpgsqlParameter("cev_won", seatAction.CevWon));
            cmd.Parameters.Add(new NpgsqlParameter("HoleCard1", (seatAction.HC1.Id != 0) ? Convert.ToInt32(seatAction.HC1.Id) : DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("HoleCard2", (seatAction.HC2.Id != 0) ? Convert.ToInt32(seatAction.HC2.Id) : DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("HCsSimpleId", (seatAction.HCsAsNumber != 0) ? seatAction.HCsAsNumber : DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("flg_open_opp", seatAction.pf_open_opp));
            cmd.Parameters.Add(new NpgsqlParameter("PreflopActionSimple", DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("p_act_1", DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("p_act_1_size", DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("p_act_2", DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("p_act_2_size", DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("p_act_3", DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("p_act_3_size", DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("p_act_4", DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("p_act_4_size", DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("p_act_5", DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("p_act_5_size", DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("p_act_6", DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("p_act_6_size", DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("chips_won", seatAction.ChipsWon));
            cmd.Parameters.Add(new NpgsqlParameter("cev_won", seatAction.CevWon));

            var outputId = cmd.ExecuteScalar();
            int seatId = int.Parse(outputId.ToString());

            importIds.SeatActionIds[seatAction.SeatNumber] = seatId;
            if (seatAction.SeatPosition == 0)
            {
                importIds.SeatActionId_BTN = seatId;
            }
            else if (seatAction.SeatPosition == 8)
            {
                importIds.SeatActionId_SB = seatId;
            }
            else if (seatAction.SeatPosition == 9)
            {
                importIds.SeatActionId_BB = seatId;
            }
            if (importIds.PlayerIds[seatAction.SeatNumber] == importIds.PlayerId_Hero)
            {
                importIds.SeatActionId_Hero = seatId;
            }

        }

        private static void ImportToHand(ref Hand hand, ref SQL_ImportIds importIds, ref NpgsqlConnection conn)
        {


            var cmd = new NpgsqlCommand(SQL_ImportQueries.sql_ImportToHand, conn);



            cmd.Parameters.Clear();
            cmd.Parameters.Add(new NpgsqlParameter("handId", importIds.HandId));
            cmd.Parameters.Add(new NpgsqlParameter("handidbysite", hand.Info.HandIdBySite));
            cmd.Parameters.Add(new NpgsqlParameter("tournamentId", importIds.TournamentId));
            cmd.Parameters.Add(new NpgsqlParameter("LevelHand", hand.Info.Level));
            cmd.Parameters.Add(new NpgsqlParameter("amt_bb", hand.Info.Amt_bb));
            cmd.Parameters.Add(new NpgsqlParameter("DateTimeHand", hand.Info.Date));
            //cmd.Parameters.Add(new NpgsqlParameter("TimeHand", model.Time));
            cmd.Parameters.Add(new NpgsqlParameter("TableIdBySite", hand.Info.TableIdBySite));
            cmd.Parameters.Add(new NpgsqlParameter("TournamentType", hand.Info.TournamentType));
            cmd.Parameters.Add(new NpgsqlParameter("HoleCard1", Convert.ToInt32(hand.Info.HC1.Id)));
            cmd.Parameters.Add(new NpgsqlParameter("HoleCard2", Convert.ToInt32(hand.Info.HC2.Id)));
            cmd.Parameters.Add(new NpgsqlParameter("FlopCard1", Convert.ToInt32(hand.Info.FC1.Id)));
            cmd.Parameters.Add(new NpgsqlParameter("FlopCard2", Convert.ToInt32(hand.Info.FC2.Id)));
            cmd.Parameters.Add(new NpgsqlParameter("FlopCard3", Convert.ToInt32(hand.Info.FC3.Id)));
            cmd.Parameters.Add(new NpgsqlParameter("TurnCard", Convert.ToInt32(hand.Info.TC.Id)));
            cmd.Parameters.Add(new NpgsqlParameter("RiverCard", Convert.ToInt32(hand.Info.RC.Id)));
            cmd.Parameters.Add(new NpgsqlParameter("TotalPot", Convert.ToInt32(hand.Info.TotalPot)));
            cmd.Parameters.Add(new NpgsqlParameter("pf_aggressors", hand.Info.pf_aggressors != null ? hand.Info.pf_aggressors : DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("pf_actors", hand.Info.pf_actors != null ? hand.Info.pf_actors : DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("cnt_players", Convert.ToInt32(hand.Info.CntPlayers)));
            cmd.Parameters.Add(new NpgsqlParameter("cnt_players_flop", Convert.ToInt32(hand.Info.CntPlayers_Flop)));
            cmd.Parameters.Add(new NpgsqlParameter("cnt_players_turn", Convert.ToInt32(hand.Info.CntPlayers_Turn)));
            cmd.Parameters.Add(new NpgsqlParameter("cnt_players_river", Convert.ToInt32(hand.Info.CntPlayers_River)));
            cmd.Parameters.Add(new NpgsqlParameter("cnt_players_showdown", Convert.ToInt32(hand.Info.CntPlayers_Showdown)));


            cmd.Parameters.Add(new NpgsqlParameter("roomId", importIds.RoomId));

            for (int i = 0; i < hand.Info.Players.Count; i++)
            {
                ImportToSeatAction(hand.SeatActions[hand.Info.Players[i]], ref importIds, ref conn);
            }



            cmd.Parameters.Add(new NpgsqlParameter("seat1ActionId", importIds.SeatActionIds[1] != 0 ? importIds.SeatActionIds[1] : DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("seat2ActionId", importIds.SeatActionIds[2] != 0 ? importIds.SeatActionIds[2] : DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("seat3ActionId", importIds.SeatActionIds[3] != 0 ? importIds.SeatActionIds[3] : DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("seat4ActionId", importIds.SeatActionIds[4] != 0 ? importIds.SeatActionIds[4] : DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("seat5ActionId", importIds.SeatActionIds[5] != 0 ? importIds.SeatActionIds[5] : DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("seat6ActionId", importIds.SeatActionIds[6] != 0 ? importIds.SeatActionIds[6] : DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("seat7ActionId", importIds.SeatActionIds[7] != 0 ? importIds.SeatActionIds[7] : DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("seat8ActionId", importIds.SeatActionIds[8] != 0 ? importIds.SeatActionIds[8] : DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("seat9ActionId", importIds.SeatActionIds[9] != 0 ? importIds.SeatActionIds[9] : DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("seat10ActionId", importIds.SeatActionIds[10] != 0 ? importIds.SeatActionIds[10] : DBNull.Value));

            cmd.Parameters.Add(new NpgsqlParameter("btnSeatActionId", importIds.SeatActionId_BTN != 0 ? importIds.SeatActionId_BTN : DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("sbSeatActionId", importIds.SeatActionId_SB != 0 ? importIds.SeatActionId_SB : DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("bbSeatActionId", importIds.SeatActionId_BB != 0 ? importIds.SeatActionId_BB : DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("heroSeatActionId", importIds.SeatActionId_Hero != 0 ? importIds.SeatActionId_Hero : DBNull.Value));

            cmd.Parameters.Add(new NpgsqlParameter("heroid", importIds.PlayerId_Hero != 0 ? importIds.PlayerId_Hero : DBNull.Value));

            cmd.ExecuteNonQuery();
        }

        private static void ImportToHoleCardsSimple(ref NpgsqlConnection conn)
        {
            //Card currCard = new ();
            for (uint i = 0; i < 170; i++)
            {
                //currCard.Id = i;
                //currCard.Name = EnumExtensionMethods.GetDescription((CardAllSimple)i);

                var cmd = new NpgsqlCommand(SQL_ImportQueries.sql_ImportHoleCardsSimpleIds, conn);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new NpgsqlParameter("hcId", Convert.ToInt32(i)));
                cmd.Parameters.Add(new NpgsqlParameter("hcAsString", EnumExtensionMethods.GetDescription((CardAllSimple)i)));

                cmd.ExecuteNonQuery();
            }            
        }

        private static void ImportToCard(ref NpgsqlConnection conn)
        {
            //Card currCard = new ();
            for (uint i = 1; i < 53; i++)
            {
                //currCard.Id = i;
                //currCard.Name = EnumExtensionMethods.GetDescription((CardAllSimple)i);

                var cmd = new NpgsqlCommand(SQL_ImportQueries.sql_ImportToCard, conn);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(new NpgsqlParameter("hcId", Convert.ToInt32(i)));
                cmd.Parameters.Add(new NpgsqlParameter("hcAsString", EnumExtensionMethods.GetDescription((CardEnum)i)));

                cmd.ExecuteNonQuery();
            }

        }
    }
}
