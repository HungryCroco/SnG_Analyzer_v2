

namespace TrackerLibrary.Queries.NoSQL
{
    /// <summary>
    /// Contains general NoSQL Queries;
    /// </summary>
    public static class NoSQL_Queries
    {
        /// <summary>
        /// Get a View containing HandIdBySite and Room from NoSQL DB used to distinguish unique Hands;
        /// </summary>
        public static string query_GetIdAndRoomFromNoSqlDb =
            @"SELECT data->'Info'->>'HandIdBySite' AS HandIdBySite, data->'Info'->>'Room' AS Room FROM hands";




    }
}
