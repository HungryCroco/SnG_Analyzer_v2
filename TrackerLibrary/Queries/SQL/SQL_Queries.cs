

namespace TrackerLibrary.Queries.SQL
{
    /// <summary>
    /// Contains general SQL Queries;
    /// </summary>
    public static class SQL_Queries
    {
        /// <summary>
        /// Get a View containing HandIdBySite and Room from SQL DB used to distinguish unique Hands;
        /// </summary>
        public static string query_GetIdAndRoomFromSqlDb =
            @"SELECT h.HandIdBySite::varchar AS HandIdBySite, r.Room AS Room 
            FROM hands h
	            INNER JOIN public.tournament ta ON ta.id = h.tournamentid
				INNER JOIN public.room r ON ta.roomId = r.id";
    }
}
