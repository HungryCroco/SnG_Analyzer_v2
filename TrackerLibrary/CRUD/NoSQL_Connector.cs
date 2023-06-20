using System.Text.Json;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;
using TrackerLibrary;
using System.Xml.Linq;
using TrackerLibrary.Queries.NoSQL;
using Newtonsoft.Json;
using System.Diagnostics;

namespace TrackerLibrary.CRUD
{
    public static class NoSQL_Connector
    {
        private static string GetConnectionString()
        {
            string connString = string.Format(@"Host={0};Port={1};User Id={2};Password={3}", GlobalConfig.server, GlobalConfig.port, GlobalConfig.user, GlobalConfig.pass);

            return connString;
        }
        private static string GetConnectionString(string db)
        {
            string connString = string.Format(@"Host={0};Port={1};User Id={2};Password={3};Database={4}", GlobalConfig.server, GlobalConfig.port, GlobalConfig.user, GlobalConfig.pass, db);

            return connString;
        }

        public static void CreateDatabase(string dbName, string tableName, string columnName)
        {
            NpgsqlConnection connection = new();
            
            
            try
            {
                connection = new NpgsqlConnection(GetConnectionString());
                var cmd_createDB = new NpgsqlCommand($"CREATE DATABASE {dbName}; ", connection);
                connection.Open();
                cmd_createDB.ExecuteNonQuery();
                
            }
            catch (Exception)
            {
            }
            try
            {
                connection = new NpgsqlConnection(GetConnectionString(dbName));
                var cmd_updateDB = new NpgsqlCommand($"CREATE TABLE {tableName}({columnName} jsonb)TABLESPACE pg_default;", connection);
                connection.Open();
                cmd_updateDB.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }
            connection.Close();
        }

        public static void InsertToNoSqlDb(string dbName, string tableName, string columnName , List<Hand> hands)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            CreateDatabase(dbName, tableName, columnName);
            List<string> serializedHandsAsJson = SerializeHands(hands.ReturnHashSetWithUniqueHands(dbName).ToList());
            
            using (NpgsqlConnection connection = new NpgsqlConnection(GetConnectionString(dbName)))
            {
                connection.Open();
                using (var importer = connection.BeginBinaryImport($"COPY {tableName} ({columnName}) FROM STDIN (FORMAT BINARY)"))
                {
                    foreach (var hand in serializedHandsAsJson)
                    {
                        importer.StartRow();
                        importer.Write(hand, NpgsqlTypes.NpgsqlDbType.Jsonb);
                    }

                    importer.Complete();
                }
                connection.Close();
            }

            Console.WriteLine("-----");
            Console.WriteLine("Import Time: " + watch.ElapsedMilliseconds / 1000.0 + "s");
        }

        public static HashSet<Hand> ReturnHashSetWithUniqueHands(this List<Hand> handsToImport, string dbName)
        {
            //List<(string, long)> oldData = new();
            var importedHandSet = new HashSet<(string Room, long HandId)> ();
            var handsToImportDistinctSet = new HashSet<Hand>();

            using (NpgsqlConnection connection = new NpgsqlConnection(GetConnectionString(dbName)))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(NoSQL_Queries.query_GetIdAndRoomFromNoSqlDb, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                            {
                                string handIdBySite = reader.GetString(0);
                                string room = reader.GetString(1);

                                //oldData.Add((room ,long.Parse(handIdBySite)));
                                importedHandSet.Add((room, long.Parse(handIdBySite)));
                            }
                        }
                    }
                }
                connection.Close();      
            }

            //var importedHandSet = new HashSet<(string Room, int HandId)>(importedHands
            //.Select(h => (h.Item1, h.Item2)));


            foreach (var hand in handsToImport)
            {
                if (!importedHandSet.Contains((hand.Info.Room, hand.Info.HandIdBySite)))
                {
                    handsToImportDistinctSet.Add(hand);
                }
            }

            //if (true)
            //{
            //    int ii = 1;
            //}

            return handsToImportDistinctSet;
        }

        private static List<string> SerializeHands(List<Hand> hands)
        {
            List<string> serializedHandsAsJson = new();

            foreach (Hand hand in hands)
            {
                serializedHandsAsJson.Add(System.Text.Json.JsonSerializer.Serialize(hand));
            }

            return serializedHandsAsJson; 
        }
    }
}
