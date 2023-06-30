using Newtonsoft.Json;
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
        [JsonProperty("Preflop")]
        public List<PlayerAction> PreFlop { get; set; }

        [JsonProperty("Flop")]
        public List<PlayerAction> Flop { get; set; }

        [JsonProperty("Turn")]
        public List<PlayerAction> Turn { get; set; }

        [JsonProperty("River")]
        public List<PlayerAction> River { get; set; }

        public AllActions()
        {
            PreFlop = new List<PlayerAction>();
            Flop = new List<PlayerAction>();
            Turn = new List<PlayerAction>();
            River = new List<PlayerAction>();       
        }
    }

    public struct PlayerAction
    {
        [JsonProperty("Act")]
        public string Act;

        [JsonProperty("Size")]
        public float Size;
        //public float AmtBefore;
        //public float ChipsInvested;

        [JsonProperty("AI")]
        public sbyte AI;

        public PlayerAction()
        {
            Act = "";
            Size = 0;
            AI = 0;
        }

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

        [JsonProperty("Actions")]
        public AllActions Actions { get; set; }

        public Card HC1 { get; set; }
        public Card HC2 { get; set; }
        public int HCsAsNumber { get; set; }
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

