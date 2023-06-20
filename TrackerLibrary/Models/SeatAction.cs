using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary;

namespace TrackerLibrary.Models
{

    public class AllActions
    {
        public List<Action> PreFlop { get; set; }
        public List<Action> Flop { get; set; }
        public List<Action> Turn { get; set; }
        public List<Action> River { get; set; }

        public AllActions()
        {
            PreFlop = new List<Action>();
            Flop = new List<Action>();
            Turn = new List<Action>();
            River = new List<Action>();       
        }
    }
    public struct Action
    {
        public string Act;
        public float Size;
        //public float AmtBefore;
        //public float ChipsInvested;
        public sbyte AI;

    }

    public class SeatAction
    {
        //public string PlayerNickname { get; set; }
        public sbyte SeatNumber { get; set; }
        public float StartingStack { get; set; }
        public float Ante { get; set; }
        public float Blind { get; set; }
        public UInt16 SeatPosition { get; set; }

        public string StreetAI { get; set; }

        public float ChipsInvested { get; set; }
        //public float ChipsInvested_Preflop { get; set; }
        //public float ChipsInvested_Flop { get; set; }
        //public float ChipsInvested_Turn { get; set; }
        //public float ChipsInvested_River { get; set; }


        public float ChipsWon { get; set; }
        public float CevWon { get; set; }
        public bool IsWinner { get; set; }


        public AllActions Actions { get; set; }

        public uint HC1 { get; set; }
        public uint HC2 { get; set; }
        public bool saw_flop { get; set; }
        public bool saw_turn { get; set; }
        public bool saw_river { get; set; }
        public bool saw_showdown { get; set; }


        public bool pf_open_opp { get; set; }

        public SeatAction()
        {
            Actions = new AllActions();
        }

    }
}

