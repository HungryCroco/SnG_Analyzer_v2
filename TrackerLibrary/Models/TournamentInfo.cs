using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    public class TournamentInfo
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public long TournamentIdBySite { get; set; }
        public string TournamentType { get; set; }
        public DateTime TournamentDate { get; set; }
        public string Currency { get; set; }
        public double AmtBuyIn { get; set; }
        public double AmtFee { get; set; }
        public double AmtPrizePoolReal { get; set; }
        public double AmtPrizePoolEv { get; set; }
        public int CountPlayers { get; set; }
        public int Seat1PlayerId { get; set; }
        public int Seat2PlayerId { get; set; }
        public int Seat3PlayerId { get; set; }

        public int TournamentWinnerId { get; set; }

        public TournamentInfo()
        {
            TournamentIdBySite = 0;
            TournamentType = "";
            AmtBuyIn = 0;   
            AmtFee = 0;
            Currency = "";
        }

    }
}