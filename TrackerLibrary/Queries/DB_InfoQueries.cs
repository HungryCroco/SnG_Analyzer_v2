

namespace TrackerLibrary.Queries
{
    /// <summary>
    /// Containes all Queries necessary to power up general Server Queries;
    /// </summary>
    public static class DB_InfoQueries
    {
        /// <summary>
        /// Get A DataView with all DB's Names and Sizes;
        /// </summary>
        public static string DB_Info_ExportDataGridView_AllDBs =
            @"SELECT datname AS DataBase , pg_size_pretty(pg_database_size(datname)) AS SIZE
            FROM pg_database;";
    }
}
