

namespace TrackerLibrary.Models
{

    /// <summary>
    /// Composited by strings, that can be concatenated together to build an NoSQL/SQL Query;
    /// </summary>
    public struct Query
    {
        /// <summary>
        /// Query used for requesting a HeatMap;
        /// </summary>
        public string MainQuery;

        /// <summary>
        /// Query used for requesting a DataGridView;
        /// </summary>
        public string DataViewQuery;

        /// <summary>
        /// Where-Conditions attached to Hero;
        /// </summary>
        public string WhereClauseHero;

        /// <summary>
        /// Where-Conditions attached to Villain;
        /// </summary>
        public string WhereClauseVillain;

        /// <summary>
        /// SeatActionId of Hero. Used only for SQL queries! Determined of the Hero's position;
        /// Possible values:
        ///     - btnSeatActionId
        ///     - sbSeatActionId
        ///     - bbSeatActionId
        /// </summary>
        public string SeatActionHero;

        /// <summary>
        /// SeatActionId of Villain. Used only for SQL queries! Determined of the Villain's position;
        /// Possible values:
        ///     - btnSeatActionId
        ///     - sbSeatActionId
        ///     - bbSeatActionId
        /// </summary>
        public string SeatActionVillain;

        /// <summary>
        /// Composited by strings, that can be concatenated together to build an NoSQL/SQL Query;
        /// </summary>
        /// <param name="mainQuery">Query used for requesting a HeatMap;</param>
        /// <param name="dataViewQuery">Query used for requesting a DataGridView;</param>
        /// <param name="whereClauseHero">Where-Conditions attached to Hero;</param>
        /// <param name="whereClauseVillain">Where-Conditions attached to Villain;</param>
        public Query(string mainQuery, string dataViewQuery, string whereClauseHero, string whereClauseVillain)
        {
            MainQuery = mainQuery;
            DataViewQuery = dataViewQuery;
            WhereClauseHero = whereClauseHero;
            WhereClauseVillain = whereClauseVillain;
            SeatActionHero = "";
            SeatActionVillain = "";
        }

        /// <summary>
        /// Composited by strings, that can be concatenated together to build an NoSQL/SQL Query;
        /// </summary>
        /// <param name="mainQuery">Query used for requesting a HeatMap;</param>
        /// <param name="dataViewQuery">Query used for requesting a DataGridView;</param>
        /// <param name="whereClauseHero">Where-Conditions attached to Hero;</param>
        /// <param name="whereClauseVillain">Where-Conditions attached to Villain;</param>
        /// <param name="seatActionHero">SeatActionId of Hero. Used only for SQL queries! Determined of the Hero's position;</param>
        /// <param name="seatActionVillain">SeatActionId of Villain. Used only for SQL queries! Determined of the Villain's position;</param>
        public Query(string mainQuery, string dataViewQuery, string whereClauseHero, string whereClauseVillain, string seatActionHero, string seatActionVillain)
        {
            MainQuery = mainQuery;
            DataViewQuery = dataViewQuery;
            WhereClauseHero = whereClauseHero;
            WhereClauseVillain = whereClauseVillain;
            SeatActionHero = seatActionHero;
            SeatActionVillain = seatActionVillain;
        }
    }

}
