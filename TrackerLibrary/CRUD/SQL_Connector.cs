using Npgsql;
using System.Diagnostics;
using TrackerLibrary.Models;
using TrackerLibrary.Queries.SQL;

namespace TrackerLibrary.CRUD
{
    /// <summary>
    /// Class containing all Methods, necessary to perform SQL CRUD operations;
    /// </summary>
    public static class SQL_Connector
    {
        /// <summary>
        /// Runs a query to import Hands to SQL DataBase;
        /// </summary>
        /// <param name="dbName">DataBase's Name;</param>
        /// <param name="hands">List of Hands to be imported;</param>
        public static void ImportHandsToSqlDb(string dbName, List<Hand> hands)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            //Creates DB if not existing;
            CreateDatabase(dbName);

            NpgsqlConnection conn = new NpgsqlConnection(GlobalConfig.GetConnectionString(dbName));
            conn.Open();

            //Filter out the not unique Hands;
            List<Hand> uniqueHands = hands.ReturnHashSetWithUniqueHands(dbName, SQL_Queries.query_GetIdAndRoomFromSqlDb, ref conn).ToList();
            //Import Hands one by one;
            foreach (var hand in uniqueHands)
            {
                ExportEntireHand(hand, ref dbName, ref conn);
            }


            conn.Close();

            Console.WriteLine("---");
            Console.WriteLine($"Import to SQL {dbName}: " + watch.ElapsedMilliseconds / 1000.0 + "s");
        }

        /// <summary>
        /// Creates an empty SQL-relational DataBase;
        /// </summary>
        /// <param name="dbName"></param>
        public static void CreateDatabase(string dbName)
        {
            NpgsqlConnection conn = new();

            //Try to run multiple queries to Create all Tables and relations one by one, if the DB is already existing will raise Exception;
            try
            {
                conn = new NpgsqlConnection(GlobalConfig.GetConnectionString());
                var cmd_createDB = new NpgsqlCommand($"CREATE DATABASE {dbName}; ", conn);


                conn.Open();
                cmd_createDB.ExecuteNonQuery();
                conn.Close();

                conn = new NpgsqlConnection(GlobalConfig.GetConnectionString(dbName));

                // Creates HandAsString Table(conatining the entire Hand as single string); 
                var cmd_CreateTable_HandAsString = new NpgsqlCommand(SQL_CreateDatabaseQueries.sql_CreateTable_HandAsString, conn);

                // Creates HoleCardsSimple(Enum CardsAllSimple) Table, this table is constant and not updated while importing;
                var cmd_CreateTable_HoleCardsSimple = new NpgsqlCommand(SQL_CreateDatabaseQueries.sql_CreateTable_HoleCardsSimple, conn);

                // Creates Card(Enum Card) Table, this table is constant and not updated while importing;
                var cmd_CreateTable_Card = new NpgsqlCommand(SQL_CreateDatabaseQueries.sql_CreateTable_Card, conn);

                // Creates Room Table, that contains the game's provider for each Hand;
                var cmd_CreateTable_Room = new NpgsqlCommand(SQL_CreateDatabaseQueries.sql_CreateTable_Room, conn);

                // Creates Player Table, that containes the PlayerNickNames of each player;
                var cmd_CreateTable_Player = new NpgsqlCommand(SQL_CreateDatabaseQueries.sql_CreateTable_Player, conn);

                // Creates Tournament Table, containing all the Information unique for a Tournament;
                var cmd_CreateTable_Tournament = new NpgsqlCommand(SQL_CreateDatabaseQueries.sql_CreateTable_Tournament, conn);

                // Creates SeatAction Table, containing all the Information unique for a SeatAction;
                var cmd_CreateTable_SeatAction = new NpgsqlCommand(SQL_CreateDatabaseQueries.sql_CreateTable_SeatAction, conn);

                // Creates the MAIN Hands Table;
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

        /// <summary>
        /// Exports an entire Hand from C# to PostgreSQL relational DB;
        /// ps: As it's relational DB import, order of importing(calling the Methods) is important;
        /// 	Correct order is:
        ///		1) Import To HoleCardsSimple (only while creating DB);
        ///		2) Import To Cards (only while creating DB);
        ///		3) Import To Room;
        ///		4) Import To Player;
        ///		5) Import To Tournament;
        ///		6) Import To HandAsString;
        ///		7) Import To SeatAction;
        ///		8) Import To Hand
        /// </summary>
        /// <param name="hand"> Hand to be exported;</param>
        /// <param name="dbName">DataBase Name;</param>
        /// <param name="conn">NPGSQL Connection;</param>
        public static void ExportEntireHand(Hand hand, ref string dbName, ref NpgsqlConnection conn)
        {
            // Creating SQL_ImportIds empty object;
            SQL_ImportIds currImportIds = new();

            // Importing to Room and Updating RoomId in SQL_ImportIds
            ImportToRoom(ref hand, ref currImportIds, ref conn);

            // Importing to Room and Updating all PlayerId's in SQL_ImportIds
            ImportToPlayer(ref hand, ref currImportIds, ref conn);

            // Importing to Room and Updating Tournament Id in SQL_ImportIds
            ImportToTournament(ref hand, ref currImportIds, ref conn);

            // Importing to Room and Updating Hand Id in SQL_ImportIds
            ImportToHandAsString(ref hand, ref currImportIds, ref conn);

            // Importing to Hand;
            ImportToHand(ref hand, ref currImportIds, ref conn);




        }

        /// <summary>
        /// Importing to Room and update the roomId;
        /// </summary>
        /// <param name="hand">Reference to Hand to be imported;</param>
        /// <param name="currHandImportIds">Reference to SQL_ImportIds, that will be updated in this Method;</param>
        /// <param name="conn">Reference to PostgreSQL Connection</param>
        /// <returns>roomId</returns>
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

        /// <summary>
        /// Importing to Player and update the playerId;
        /// </summary>
        /// <param name="hand">Reference to Hand to be imported;</param>
        /// <param name="currHandImportIds">Reference to SQL_ImportIds, that will be updated in this Method;</param>
        /// <param name="conn">Reference to PostgreSQL Connection</param>
        /// <returns>playerId</returns>
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

        /// <summary>
        /// Importing to Tournament and update the tournamentId;
        /// </summary>
        /// <param name="hand">Reference to Hand to be imported;</param>
        /// <param name="currHandImportIds">Reference to SQL_ImportIds, that will be updated in this Method;</param>
        /// <param name="conn">Reference to PostgreSQL Connection</param>
        /// <returns>tournamentId</returns>
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

        /// <summary>
        /// Importing to HandAsString and update the handId;
        /// </summary>
        /// <param name="hand">Reference to Hand to be imported;</param>
        /// <param name="currHandImportIds">Reference to SQL_ImportIds, that will be updated in this Method;</param>
        /// <param name="conn">Reference to PostgreSQL Connection</param>
        /// <returns>handId</returns>
        private static int ImportToHandAsString(ref Hand hand, ref SQL_ImportIds currHandImportIds, ref NpgsqlConnection conn)
        {
            var cmd = new NpgsqlCommand(SQL_ImportQueries.sql_ImportHandAsAtring, conn);

            cmd.Parameters.Clear();
            cmd.Parameters.Add(new NpgsqlParameter("fullHand", hand.Info.FullHandAsString));

            var output = cmd.ExecuteScalar();

            int outputId = int.Parse(output.ToString());
            currHandImportIds.HandId = outputId;

            return outputId;
        }

        /// <summary>
        /// Imports a SeatAction to the SQL SeatAction Table and updates all seatAction's Ids in the SQL_ImportIds;
        /// </summary>
        /// <param name="seatAction">SeatAction, that will be imported</param>
        /// <param name="currHandImportIds">Reference to SQL_ImportIds, that will be updated in this Method;</param>
        /// <param name="conn">Reference to PostgreSQL Connection</param>
        public static void ImportToSeatAction(SeatAction seatAction, ref SQL_ImportIds currHandImportIds, ref NpgsqlConnection conn)
        {
            var cmd = new NpgsqlCommand(SQL_ImportQueries.sql_ImportSeatAction, conn);

            //Adding Parameters;
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new NpgsqlParameter("PlayerId", currHandImportIds.PlayerIds[seatAction.SeatNumber]));
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

            //Importing and getting the seatActionID
            var outputId = cmd.ExecuteScalar();
            int seatId = int.Parse(outputId.ToString());

            //Updating the seatActionIds in SQL_ImportIds;
            currHandImportIds.SeatActionIds[seatAction.SeatNumber] = seatId;
            if (seatAction.SeatPosition == 0)
            {
                currHandImportIds.SeatActionId_BTN = seatId;
            }
            else if (seatAction.SeatPosition == 8)
            {
                currHandImportIds.SeatActionId_SB = seatId;
            }
            else if (seatAction.SeatPosition == 9)
            {
                currHandImportIds.SeatActionId_BB = seatId;
            }
            if (currHandImportIds.PlayerIds[seatAction.SeatNumber] == currHandImportIds.PlayerId_Hero)
            {
                currHandImportIds.SeatActionId_Hero = seatId;
            }

        }

        /// <summary>
        /// Importing to Hands;
        /// </summary>
        /// <param name="hand">Reference to Hand to be imported;</param>
        /// <param name="currHandImportIds">Reference to SQL_ImportIds, that will be updated in this Method;</param>
        /// <param name="conn">Reference to PostgreSQL Connection;</param>
        private static void ImportToHand(ref Hand hand, ref SQL_ImportIds currHandImportIds, ref NpgsqlConnection conn)
        {


            var cmd = new NpgsqlCommand(SQL_ImportQueries.sql_ImportToHand, conn);


            //Adding Parameters;
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new NpgsqlParameter("handId", currHandImportIds.HandId));
            cmd.Parameters.Add(new NpgsqlParameter("handidbysite", hand.Info.HandIdBySite));
            cmd.Parameters.Add(new NpgsqlParameter("tournamentId", currHandImportIds.TournamentId));
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

            cmd.Parameters.Add(new NpgsqlParameter("roomId", currHandImportIds.RoomId));

            //Importing all SeatAction for the Hand:
            for (int i = 0; i < hand.Info.Players.Count; i++)
            {
                ImportToSeatAction(hand.SeatActions[hand.Info.Players[i]], ref currHandImportIds, ref conn);
            }


            //Updating the Parameters containing SeatActionId's:
            cmd.Parameters.Add(new NpgsqlParameter("seat1ActionId", currHandImportIds.SeatActionIds[1] != 0 ? currHandImportIds.SeatActionIds[1] : DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("seat2ActionId", currHandImportIds.SeatActionIds[2] != 0 ? currHandImportIds.SeatActionIds[2] : DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("seat3ActionId", currHandImportIds.SeatActionIds[3] != 0 ? currHandImportIds.SeatActionIds[3] : DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("seat4ActionId", currHandImportIds.SeatActionIds[4] != 0 ? currHandImportIds.SeatActionIds[4] : DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("seat5ActionId", currHandImportIds.SeatActionIds[5] != 0 ? currHandImportIds.SeatActionIds[5] : DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("seat6ActionId", currHandImportIds.SeatActionIds[6] != 0 ? currHandImportIds.SeatActionIds[6] : DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("seat7ActionId", currHandImportIds.SeatActionIds[7] != 0 ? currHandImportIds.SeatActionIds[7] : DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("seat8ActionId", currHandImportIds.SeatActionIds[8] != 0 ? currHandImportIds.SeatActionIds[8] : DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("seat9ActionId", currHandImportIds.SeatActionIds[9] != 0 ? currHandImportIds.SeatActionIds[9] : DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("seat10ActionId", currHandImportIds.SeatActionIds[10] != 0 ? currHandImportIds.SeatActionIds[10] : DBNull.Value));

            cmd.Parameters.Add(new NpgsqlParameter("btnSeatActionId", currHandImportIds.SeatActionId_BTN != 0 ? currHandImportIds.SeatActionId_BTN : DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("sbSeatActionId", currHandImportIds.SeatActionId_SB != 0 ? currHandImportIds.SeatActionId_SB : DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("bbSeatActionId", currHandImportIds.SeatActionId_BB != 0 ? currHandImportIds.SeatActionId_BB : DBNull.Value));
            cmd.Parameters.Add(new NpgsqlParameter("heroSeatActionId", currHandImportIds.SeatActionId_Hero != 0 ? currHandImportIds.SeatActionId_Hero : DBNull.Value));

            cmd.Parameters.Add(new NpgsqlParameter("heroid", currHandImportIds.PlayerId_Hero != 0 ? currHandImportIds.PlayerId_Hero : DBNull.Value));

            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Importing to HoleCardsSimple ( Enum AllCardSimple);
        /// </summary>
        /// <param name="conn">Reference to PostgreSQL Connection;</param>
        private static void ImportToHoleCardsSimple(ref NpgsqlConnection conn)
        {
            //looping trough AllCardsSimple Enum and importing Id and Description;
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

        /// <summary>
        /// Importing to HoleCardsSimple ( Enum CardEnum);
        /// </summary>
        /// <param name="conn">Reference to PostgreSQL Connection;</param>
        private static void ImportToCard(ref NpgsqlConnection conn)
        {
            // looping trough CardEnum and importing Id and Description;
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
