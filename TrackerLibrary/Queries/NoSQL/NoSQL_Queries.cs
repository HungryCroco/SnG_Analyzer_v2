using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Queries.NoSQL
{
    public partial class NoSQL_Queries
    {
        public static string query_GetIdAndRoomFromNoSqlDb =
            @"SELECT data->'Info'->>'HandIdBySite' AS HandIdBySite, data->'Info'->>'Room' AS Room FROM hands";




    }
}
