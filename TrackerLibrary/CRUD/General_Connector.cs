using Npgsql;
using TrackerLibrary.Models;


namespace TrackerLibrary.CRUD
{
    /// <summary>
    /// Contains Methods, tht are used for performing CRUD operations in multiple ways(SQL, NoSQL, File);
    /// </summary>
    public static class General_Connector
    {
        /// <summary>
        /// Runs a query to get the HandIdBySite and Room for each already imported Hand and deletes all of the already imported Hands of the HashSet;
        /// </summary>
        /// <param name="handsToImport">List with Hands that will be imported</param>
        /// <param name="dbName">DataBase(Write) Name</param>
        /// <param name="getIdAndRoomQuery">Query returning all (HandIdBySite , Room) entries</param>
        /// <param name="conn">PostgreSQL connection</param>
        /// <returns>A HashSet of unique Hands, that are not already existing in the DB;</returns>
        public static HashSet<Hand> ReturnHashSetWithUniqueHands(this List<Hand> handsToImport, string dbName, string getIdAndRoomQuery, ref NpgsqlConnection conn)
        {
            
            var importedHandSet = new HashSet<(string Room, long HandId)>(); //already imported Hands;
            var handsToImportDistinctSet = new HashSet<Hand>(); //unique Hands

            
            using (NpgsqlCommand command = new NpgsqlCommand(getIdAndRoomQuery, conn))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0))
                        {
                            string handIdBySite = reader.GetString(0);
                            string room = reader.GetString(1);

                            //update the HashSet of already imported Hands;
                            importedHandSet.Add((room, long.Parse(handIdBySite)));
                        }
                    }
                }
            }  

            // Check if the Hand is in the importedHands List and if not add id to the toBeImported HashSet;
            foreach (var hand in handsToImport)
            {
                if (!importedHandSet.Contains((hand.Info.Room, hand.Info.HandIdBySite)))
                {
                    handsToImportDistinctSet.Add(hand);
                }
            }


            return handsToImportDistinctSet;
        }

        /// <summary>
        /// Deletes all entries from a table;
        /// </summary>
        /// <param name="dbName">DataBase Name;</param>
        /// <param name="tableName">Table's Name;</param>
        public static void DeleteTable(this string dbName, string tableName)
        {

            using (NpgsqlConnection connection = new NpgsqlConnection(GlobalConfig.GetConnectionString(dbName)))
            {
                connection.Open();


                var cmd_updateDB = new NpgsqlCommand($"DROP TABLE IF EXISTS {tableName};", connection);
                cmd_updateDB.ExecuteNonQuery();
                connection.Close();
            }
        }

    }
}
