

namespace TrackerLibrary.Models
{
    /// <summary>
    /// This Class is used to power up the importing to SQL DataBase, as it's relational DB and some tables are related;
    /// </summary>
    public class SQL_ImportIds
    {
        /// <summary>
        /// List with Ids of all SeatActions;
        /// </summary>
        public List<int> SeatActionIds { get; set; }

        /// <summary>
        /// List with Ids of all Players;
        /// </summary>
        public List<int> PlayerIds { get; set; } // ExportToPlayer()

        /// <summary>
        /// Id of the Room;
        /// </summary>
        public int RoomId { get; set; }

        /// <summary>
        /// Id of the Hand;
        /// </summary>
        public int HandId { get; set; }

        /// <summary>
        /// Id of the Tournament;
        /// </summary>
        public int TournamentId { get; set; }

        /// <summary>
        /// Id of hero's SeatAction;
        /// </summary>
        public int SeatActionId_Hero { get; set; }

        /// <summary>
        /// Id of the SeatAction that is on the Button;
        /// </summary>
        public int SeatActionId_BTN { get; set; }

        /// <summary>
        /// Id of the SeatAction that is on the SmallBlind;
        /// </summary>
        public int SeatActionId_SB { get; set; }

        /// <summary>
        /// Id of the SeatAction that is on the BigBlind;
        /// </summary>
        public int SeatActionId_BB { get; set; }

        /// <summary>
        /// Id of Hero;
        /// </summary>
        public int PlayerId_Hero { get; set; }

        /// <summary>
        /// Contains Id's that are needed to perform an import to SQL (relational) DB;
        /// </summary>
        public SQL_ImportIds()
        {
            SeatActionIds = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            PlayerIds = new List<int>() { 0,0,0,0,0,0,0,0,0,0,0};
        }
    }
}
