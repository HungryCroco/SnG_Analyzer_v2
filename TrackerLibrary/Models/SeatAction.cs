using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary;

namespace TrackerLibrary.Models
{
    public class PlayerActionList : List<PlayerAction>
    {
        public override string ToString()
        {
            // Implement your custom logic here to convert the list to a string representation
            // You can iterate over the list and concatenate the string representation of each item

            // Example implementation:
            // Assuming PlayerAction has properties 'PlayerName' and 'ActionType'
            string result = "";
            foreach (PlayerAction playerAction in this)
            {
                if (playerAction.Act == "checks")
                {
                    result = "X";
                }
                else if (playerAction.Act == "calls")
                {
                    result = "C" + playerAction.Size;
                }
                else if (playerAction.Act == "bets")
                {
                    result = "B" + playerAction.Size;
                }
                else if (playerAction.Act == "raises")
                {
                    result = "R" + playerAction.Size;
                }

                if (playerAction.AI == 1)
                {
                    result += "ai";
                }
            }
            return result;
        }
    }

    public class AllActions
    {
        [JsonProperty("Preflop")]
        public PlayerActionList PreFlop { get; set; }

        [JsonProperty("Flop")]
        public PlayerActionList Flop { get; set; }

        [JsonProperty("Turn")]
        public PlayerActionList Turn { get; set; }

        [JsonProperty("River")]
        public PlayerActionList River { get; set; }

        public AllActions()
        {
            PreFlop = new ();
            Flop = new ();
            Turn = new ();
            River = new ();       
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
        public uint SeatPosition { get; set; }

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

