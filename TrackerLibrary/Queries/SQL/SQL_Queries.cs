using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Queries.SQL
{
    public static class SQL_Queries
    {
        public static string query_GetIdAndRoomFromSqlDb =
            @"SELECT h.HandIdBySite::varchar AS HandIdBySite, r.Room AS Room 
            FROM hands h
	            INNER JOIN public.tournament ta ON ta.id = h.tournamentid
				INNER JOIN public.room r ON ta.roomId = r.id";
    }
}
