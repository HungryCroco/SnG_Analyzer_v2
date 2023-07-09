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

namespace TrackerLibrary.CRUD
{
    public static class SQL_Connector
    {
        public static void ImportHandsToSqlDb(string dbName, List<Hand> hands)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            CreateDatabase(dbName);

            NpgsqlConnection connection = new NpgsqlConnection(GlobalConfig.GetConnectionString(dbName));
            connection.Open();

            foreach (var hand in hands)
            {
            ExportEntireHand(hand, ref dbName);
            }


            connection.Close();

            Console.WriteLine("-----");
            Console.WriteLine("Import Time: " + watch.ElapsedMilliseconds / 1000.0 + "s");
        }

        public static void CreateDatabase(string dbName)
        {
            NpgsqlConnection connection = new();

            try
            {
                connection = new NpgsqlConnection(GlobalConfig.GetConnectionString());
                var cmd_createDB = new NpgsqlCommand($"CREATE DATABASE {dbName}; ", connection);
                

                connection.Open();
                cmd_createDB.ExecuteNonQuery();
                connection.Close();

                connection = new NpgsqlConnection(GlobalConfig.GetConnectionString(dbName));

                var cmd_CreateTable_HandAsString = new NpgsqlCommand(SQL_CreateDatabaseQueries.sql_CreateTable_HandAsString, connection);
                var cmd_CreateTable_HoleCardsSimple = new NpgsqlCommand(SQL_CreateDatabaseQueries.sql_CreateTable_HoleCardsSimple, connection);
                var cmd_CreateTable_Room = new NpgsqlCommand(SQL_CreateDatabaseQueries.sql_CreateTable_Room, connection);
                var cmd_CreateTable_Player = new NpgsqlCommand(SQL_CreateDatabaseQueries.sql_CreateTable_Player, connection);
                var cmd_CreateTable_Tournament = new NpgsqlCommand(SQL_CreateDatabaseQueries.sql_CreateTable_Tournament, connection);
                var cmd_CreateTable_SeatAction = new NpgsqlCommand(SQL_CreateDatabaseQueries.sql_CreateTable_SeatAction, connection);
                var cmd_CreateTable_Hands = new NpgsqlCommand(SQL_CreateDatabaseQueries.sql_CreateTable_Hands, connection);

                connection.Open();

                cmd_CreateTable_HandAsString.ExecuteNonQuery();
                cmd_CreateTable_HoleCardsSimple.ExecuteNonQuery();
                cmd_CreateTable_Room.ExecuteNonQuery();
                cmd_CreateTable_Player.ExecuteNonQuery();
                cmd_CreateTable_Tournament.ExecuteNonQuery();
                cmd_CreateTable_SeatAction.ExecuteNonQuery();
                cmd_CreateTable_Hands.ExecuteNonQuery();  
            }
            catch (Exception)
            {
            }
            connection.Close();
        }

        public static void ExportEntireHand(Hand hand, ref string dbName)
        {
            NpgsqlConnection conn = new NpgsqlConnection(GlobalConfig.GetConnectionString(dbName));

            conn.Open();
            SQL_ImportIds currImportIds = new();

            ImportToRoom(ref hand, ref currImportIds, ref conn);
            ImportToPlayer(ref hand, ref currImportIds, ref conn);
            ImportToTournament(ref hand, ref currImportIds, ref conn);
            
            ImportToHandAsString(ref hand, ref currImportIds, ref conn);

            ImportToHand(ref hand, ref currImportIds, ref conn);

            conn.Close();


             
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

        private static  int ImportToTournament(ref Hand hand, ref SQL_ImportIds currHandImportIds, ref NpgsqlConnection conn)
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

        public static void ImportToSeatAction(SeatAction seatAction, ref SQL_ImportIds importIds , int seatActionIndex , ref NpgsqlConnection conn)
        {
            var cmd = new NpgsqlCommand(SQL_ImportQueries.sql_ImportSeatAction, conn);

            cmd.Parameters.Clear();
            cmd.Parameters.Add(new NpgsqlParameter("PlayerId", importIds.PlayerIds[seatActionIndex]));
            cmd.Parameters.Add(new NpgsqlParameter("SeatNumber", Convert.ToInt32(seatAction.SeatNumber)));
            cmd.Parameters.Add(new NpgsqlParameter("Position", Convert.ToInt32(seatAction.SeatPosition)));
            cmd.Parameters.Add(new NpgsqlParameter("StartingStack", seatAction.StartingStack));
            cmd.Parameters.Add(new NpgsqlParameter("PreflopAction", seatAction.Actions.PreFlop.ToString()));
            cmd.Parameters.Add(new NpgsqlParameter("FlopAction", seatAction.Actions.Flop.ToString()));
            cmd.Parameters.Add(new NpgsqlParameter("TurnAction", seatAction.Actions.Turn.ToString()));
            cmd.Parameters.Add(new NpgsqlParameter("RiverAction", seatAction.Actions.River.ToString()));
            //cmd.Parameters.Add(new NpgsqlParameter("IsWinner", seat.IsWinner));
            //cmd.Parameters.Add(new NpgsqlParameter("AmountWon", seatAction.));
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
            cmd.Parameters.Add(new NpgsqlParameter("chips_Won", DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("cEV_Won", DBNull.Value));

            var outputId = cmd.ExecuteScalar();
            int seatId = int.Parse(outputId.ToString());

            importIds.SeatActionIds.Add(seatId);
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
            if (importIds.PlayerIds[seatActionIndex] == importIds.PlayerId_Hero)
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


            cmd.Parameters.Add(new NpgsqlParameter("roomId", importIds.RoomId));

            for (int i = 0; i < hand.Info.Players.Count; i++)
            {
                ImportToSeatAction(hand.SeatActions[hand.Info.Players[i]], ref importIds, i, ref conn);
            }


            
            cmd.Parameters.Add(new NpgsqlParameter("seat1ActionId", DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("seat2ActionId", DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("seat3ActionId", DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("seat4ActionId", DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("seat5ActionId", DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("seat6ActionId", DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("seat7ActionId", DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("seat8ActionId", DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("seat9ActionId", DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("seat10ActionId", DBNull.Value));

            cmd.Parameters.Add(new NpgsqlParameter("btnSeatActionId", DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("sbSeatActionId", DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("bbSeatActionId", DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("heroSeatActionId", DBNull.Value));

            cmd.Parameters.Add(new NpgsqlParameter("heroid", importIds.PlayerId_Hero));

            cmd.ExecuteNonQuery();
        }

    } 
}
