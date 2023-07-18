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
using System.Data;

namespace TrackerLibrary.CRUD
{
    /// <summary>
    /// Class containing all Methods, necessary to perform NoSQL CRUD operations;
    /// </summary>
    public static class NoSQL_Connector
    {
        /// <summary>
        /// Creates a DataBase if not existing;
        /// </summary>
        /// <param name="dbName">DataBase's Name;</param>
        /// <param name="tableName">Name of the Table;</param>
        /// <param name="columnName">Name of the Column(jsonb);</param>
        public static void CreateDatabase(string dbName, string tableName, string columnName)
        {
            NpgsqlConnection connection = new();
            
            // Try to Create the DB; If already existing will raise an exception;
            try
            {
                connection = new NpgsqlConnection(GlobalConfig.GetConnectionString());
                var cmd_createDB = new NpgsqlCommand($"CREATE DATABASE {dbName}; ", connection);
                connection.Open();
                cmd_createDB.ExecuteNonQuery();
                
            }
            catch (Exception)
            {
            }

            // Try to Create the Table; If already existing will raise an exception;
            try
            {
                connection = new NpgsqlConnection(GlobalConfig.GetConnectionString(dbName));
                var cmd_updateDB = new NpgsqlCommand($"CREATE TABLE {tableName}({columnName} jsonb)TABLESPACE pg_default;", connection);
                connection.Open();
                cmd_updateDB.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }

            // Try to Create the Column; If already existing will raise an exception;
            try
            {
                connection = new NpgsqlConnection(GlobalConfig.GetConnectionString(dbName));
                var cmd_updateDB = new NpgsqlCommand($"ALTER TABLE {tableName} ADD COLUMN {columnName} jsonb;", connection);
                connection.Open();
                cmd_updateDB.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }
            connection.Close();
        }

        /// <summary>
        /// Creates a DashBoard Table if not existing(used for saving DashBoard Models);
        /// </summary>
        /// <param name="dbName">DataBase's Name;</param>
        /// <param name="tableName">Name of the Table;</param>
        public static void CreateDashBoardTable(string dbName, string tableName)
        {
            NpgsqlConnection connection = new();

            // Try to Create the DB; If already existing will raise an exception;
            try
            {
                connection = new NpgsqlConnection(GlobalConfig.GetConnectionString(dbName));
                var cmd_updateDB = new NpgsqlCommand($"CREATE TABLE {tableName}(player text, tourneyType text, data jsonb)TABLESPACE pg_default;", connection);
                connection.Open();
                cmd_updateDB.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }

            connection.Close();
        }

        /// <summary>
        /// Run an insert query to NoSQL DB;
        /// </summary>
        /// <param name="dbName">DataBase's Name;</param>
        /// <param name="tableName">Name of the Table;</param>
        /// <param name="columnName">Name of the Column(jsonb);</param>
        /// <param name="hands">List of Hands to be inserted;</param>
        public static void InsertHandsToNoSqlDb(string dbName, string tableName, string columnName , List<Hand> hands)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            //Creating DB if not existing;
            CreateDatabase(dbName, tableName, columnName);


            NpgsqlConnection conn = new NpgsqlConnection(GlobalConfig.GetConnectionString(dbName));
            
            conn.Open();

            // Serializing the Hands and deleting the NON-unique Hands;
            List<string> serializedHandsAsJson = SerializeHands(hands.ReturnHashSetWithUniqueHands(dbName, NoSQL_Queries.query_GetIdAndRoomFromNoSqlDb, ref conn).ToList());

            // Importing the Hands;
            using (var importer = conn.BeginBinaryImport($"COPY {tableName} ({columnName}) FROM STDIN (FORMAT BINARY)"))
            {
                foreach (var hand in serializedHandsAsJson)
                {
                    importer.StartRow();
                    importer.Write(hand, NpgsqlTypes.NpgsqlDbType.Jsonb);
                }

                importer.Complete();
            }
            conn.Close();
            

            Console.WriteLine("---");
            Console.WriteLine($"Import to NoSQL {dbName}: " + watch.ElapsedMilliseconds / 1000.0 + "s");
        }

        /// <summary>
        /// Inserting a DashBoardModel as JSON to the DB;
        /// </summary>
        /// <param name="dbName">DataBase's Name;</param>
        /// <param name="tableName">Name of the Table;</param>
        /// <param name="activePlayer">Active Player;</param>
        /// <param name="tourneyType">Tournament Type;</param>
        /// <param name="dashboardModel">DashBoardModel;</param>
        public static void InsertDashBoardModelToNoSqlDb(string dbName, string tableName, string activePlayer, string tourneyType, DashBoardModel dashboardModel)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            // Creating the DashBoard table if not existing;
            CreateDashBoardTable(dbName, tableName);

            // Serializing the DashBoardModel to JSON;
            var serializedDashBoardModel = System.Text.Json.JsonSerializer.Serialize(dashboardModel);

            using (NpgsqlConnection connection = new NpgsqlConnection(GlobalConfig.GetConnectionString(dbName)))
            {
                connection.Open();

                // Importing to the DB;
                using (var importer = connection.BeginBinaryImport($"COPY {tableName} (player, tourneyType, data) FROM STDIN (FORMAT BINARY)"))
                {
                    importer.StartRow();
                    importer.Write(activePlayer, NpgsqlTypes.NpgsqlDbType.Text);
                    importer.Write(tourneyType, NpgsqlTypes.NpgsqlDbType.Text);
                    importer.Write(serializedDashBoardModel, NpgsqlTypes.NpgsqlDbType.Jsonb);

                    importer.Complete();
                }
                connection.Close();
            }
        }

        /// <summary>
        /// Serializing Hands to JSON;
        /// </summary>
        /// <param name="hands">List of Hands to be serialized;</param>
        /// <returns></returns>
        public static List<string> SerializeHands(List<Hand> hands)
        {
            // List of string of serialized Hands;
            List<string> serializedHandsAsJson = new();

            // Adding the JSON-Converter-Options, needed to serialize the non standart objects;
            var options = new JsonSerializerOptions();
            options.Converters.Add(new PlayerActionListConverter());
            options.Converters.Add(new FullActionListConverter());

            //Serializing the Hands one by one and adding to the List of serialized Hands;
            foreach (Hand hand in hands)
            {
                serializedHandsAsJson.Add(System.Text.Json.JsonSerializer.Serialize(hand, options));
            }

            return serializedHandsAsJson; 
        }

        ///// <summary>
        ///// Quering a JSON of CevModel and deserializing it to List of CevModel;
        ///// </summary>
        ///// <param name="tournamentType">Tournament Type;</param>
        ///// <param name="pos"></param>
        ///// <param name="sqlQuery"></param>
        ///// <param name="dbName"></param>
        ///// <returns>Deserialized List of CevModel</returns>
        //public static List<CevModel> GetCevByPosParallel(string tournamentType, string pos, string sqlQuery, string dbName)
        //{
        //    string output;

        //    using (var conn = new NpgsqlConnection(GlobalConfig.GetConnectionString(dbName)))
        //    {
        //        conn.Open();
        //        using (var cmd = new NpgsqlCommand(sqlQuery, conn))
        //        {
        //            cmd.Parameters.Clear();
        //            //_cmd.Parameters.Add(new NpgsqlParameter("activePlayer", playerNickName));
        //            cmd.Parameters.Add(new NpgsqlParameter("tourneyType", tournamentType));
        //            cmd.Parameters.Add(new NpgsqlParameter("pos", pos));

        //            output = cmd.ExecuteScalar().ToString();
        //        }
        //        conn.Close();
        //    }

        //    return JsonConvert.DeserializeObject<List<CevModel>>(output);
        //}

        /// <summary>
        /// Quering a JSON of generic Type and deserializing it to List of the generic Type;
        /// </summary>
        /// <typeparam name="T">generic Type;</typeparam>
        /// <param name="sqlQuery">SQL Query;</param>
        /// <param name="dbName">DataBase Name;</param>
        /// <returns></returns>
        public static List<T> GetCevChartParallel<T>(this string sqlQuery, string dbName)
        {
            string output;

            using (var conn = new NpgsqlConnection(GlobalConfig.GetConnectionString(dbName)))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(sqlQuery, conn))
                {
                    cmd.CommandTimeout = 1000;
                    output = cmd.ExecuteScalar().ToString();
                    
                }
                conn.Close();
            }

            return JsonConvert.DeserializeObject<List<T>>(output);
        }

        /// <summary>
        /// Run a query to get a PostgreSQL View;
        /// </summary>
        /// <param name="sqlQuery">SQL Query;</param>
        /// <param name="dbName">DataBase Name;</param>
        /// <returns>DataTable, containing the PostgreSQL View;</returns>
        public static DataTable GetView(this string sqlQuery, string dbName)
        {
            DataTable dt = new DataTable();
            try
            {
                using (var conn = new NpgsqlConnection(GlobalConfig.GetConnectionString(dbName)))
                {
                    conn.Open();
                    var cmd = new NpgsqlCommand(sqlQuery, conn); ;
                    NpgsqlDataAdapter da = default(NpgsqlDataAdapter);


                    try
                    {
                        da = new NpgsqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(dt);

                        return dt;
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show("An error occured: " + ex.Message, "Perform CRUD Operations failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dt = null;
                    }
                    conn.Close();
                }
            }
            catch (Exception)
            {
            }
            
            return dt;
        }

        /// <summary>
        /// Run a query to get a DashBoardModel from PostgreSQL;
        /// </summary>
        /// <param name="activePlayer">Active Player;</param>
        /// <param name="tourneyType">Tournament Type;</param>
        /// <param name="dbName">DataBase Name;</param>
        /// <returns>A DashBoardModel filtered by the given params;</returns>
        public static DashBoardModel GetDashBoardModel(string activePlayer, string tourneyType, string dbName)
        {
            string output;
            //Try to run the query, if the DashBoardModel with the given params or Table is not existing will return an empty DashBoardModel;
            try
            {
                using (var _conn = new NpgsqlConnection(GlobalConfig.GetConnectionString(dbName)))
                {
                    _conn.Open();
                    using (var _cmd = new NpgsqlCommand($"SELECT data FROM dashboard WHERE player = '{activePlayer}' AND tourneytype = '{tourneyType}';", _conn))
                    {
                        output = _cmd.ExecuteScalar().ToString();
                    }
                    _conn.Close();
                }

                return JsonConvert.DeserializeObject<DashBoardModel>(output);
            }
            catch (Exception)
            {
            }

            return new DashBoardModel();
        }
    }
}
