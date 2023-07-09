using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TrackerLibrary.Models
{

    public struct Card
    {
        public uint Id { get; set; }
        public string Name { get; set; }
    }
    public struct HoleCards
    {
        public Card HC1 { get; set; }
        public Card HC2 { get; set; }
    }
    public class HandInfo
    {
        //public int Id;

        public long HandIdBySite { get; set; }

        public long TournamentIdBySite { get; set; }

        public string Currency { get; set; }

        public double BuyIn { get; set; }

        public double Fee { get; set; }

        public string TournamentType { get; set; }

        public string Level { get; set; }

        public float Amt_bb { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan Time { get; set; }

        public string TableIdBySite { get; set; }

        public float TotalPot { get; set; }

        public List<float> SidePots { get; set; }

        public string FullHandAsString { get; set; }

        public string Room { get; set; }

        public string Hero { get; set; }

        public sbyte SeatBtn { get; set; }

        public uint CntPlayers { get; set; }
        public uint CntPlayers_Flop { get; set; }
        public uint CntPlayers_Turn { get; set; }
        public uint CntPlayers_River { get; set; }
        public uint CntPlayers_Showdown { get; set; }
        public List<string> Players { get; set; }
        public string pf_aggressors { get; set; }
        public string pf_actors { get; set; }


        public List<(string, HoleCards)> SawShowdown_Players { get; set; }
        public string StreetAI { get; set; }


        public float PfPot { get; set; }
        public float FlopPot { get; set; }
        public float TurnPot { get; set; }
        public float RiverPot { get; set; }

        public Card HC1 { get; set; }
        public Card HC2 { get; set; }
        public Card FC1 { get; set; }
        public Card FC2 { get; set; }
        public Card FC3 { get; set; }
        public Card TC { get; set; }
        public Card RC { get; set; }

    }
}
