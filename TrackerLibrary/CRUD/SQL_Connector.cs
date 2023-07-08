using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Queries.SQL;

namespace TrackerLibrary.CRUD
{
    public static class SQL_Connector
    {
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
    }
}
