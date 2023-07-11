using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    public class SQL_ImportIds
    {
        public List<int> SeatActionIds { get; set; }
        public List<int> PlayerIds { get; set; } // ExportToPlayer()

        public int RoomId { get; set; }

        public int HandId { get; set; }
        public int TournamentId { get; set; }
        public int SeatActionId_Hero { get; set; }
        public int SeatActionId_BTN { get; set; }
        public int SeatActionId_SB { get; set; }
        public int SeatActionId_BB { get; set; }
        public int PlayerId_Hero { get; set; }

        public SQL_ImportIds()
        {
            SeatActionIds = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            PlayerIds = new List<int>() { 0,0,0,0,0,0,0,0,0,0,0};
        }
    }
}
