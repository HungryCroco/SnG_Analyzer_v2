using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Queries
{
    public static class DB_InfoQueries
    {
        public static string DB_Info_ExportDataGridView_AllDBs =
            @"SELECT datname AS DataBase , pg_size_pretty(pg_database_size(datname)) AS SIZE
            FROM pg_database;";
    }
}
