using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;
using TrackerLibrary.Queries.NoSQL;

namespace TrackerLibrary.CRUD
{
    public static class General_Connector
    {

        public static HashSet<Hand> ReturnHashSetWithUniqueHands(this List<Hand> handsToImport, string dbName, string getIdAndRoomQuery, ref NpgsqlConnection conn)
        {
            //List<(string, long)> oldData = new();
            var importedHandSet = new HashSet<(string Room, long HandId)>();
            var handsToImportDistinctSet = new HashSet<Hand>();

            
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

                            //oldData.Add((room ,long.Parse(handIdBySite)));
                            importedHandSet.Add((room, long.Parse(handIdBySite)));
                        }
                    }
                }
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
    }
}
