using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary;

namespace TrackerLibrary.Models
{
    /// <summary>
    /// Overritten List<PlayerAction> that implements ToString();
    /// </summary>
    public class PlayerActionList : List<PlayerAction>
    {
        /// <summary>
        /// Writes every Action done by a player in a single street;
        /// Shorthen: 
        ///     - X = "checks"
        ///     - C = "calls"
        ///     - B = "bets"
        ///     - R = "raises"
        ///     - F = "folds"
        ///     - ai = "and is all-in";
        /// </summary>
        /// <returns>String containg all player Actions(shorten) + Sizes;</returns>
        public override string ToString()
        {
            
            string result = "";
            foreach (PlayerAction playerAction in this)
            {
                if (playerAction.Act == "checks")
                {
                    result += "X";
                }
                else if (playerAction.Act == "calls")
                {
                    result += "C" + playerAction.Size;
                }
                else if (playerAction.Act == "bets")
                {
                    result += "B" + playerAction.Size;
                }
                else if (playerAction.Act == "raises")
                {
                    result += "R" + playerAction.Size;
                }
                else if (playerAction.Act == "folds")
                {
                    result += "F";
                }

                if (playerAction.AI == 1)
                {
                    result += "ai";
                }
            }
            return result;
        }
    }

    /// <summary>
    /// Contains List<PlayerAction> for each Street:
    ///     - PreFlop
    ///     - Flop
    ///     - Turn
    ///     - River;
    /// </summary>
    public class AllActions
    {
        /// <summary>
        /// Contains all PlayerAction's done by the player PreFlop;
        /// </summary>
        [JsonProperty("Preflop")]
        public PlayerActionList PreFlop { get; set; }

        /// <summary>
        /// Contains all PlayerAction's done by the player on the Flop;
        /// </summary>
        [JsonProperty("Flop")]
        public PlayerActionList Flop { get; set; }

        /// <summary>
        /// Contains all PlayerAction's done by the player on the Turn;
        /// </summary>
        [JsonProperty("Turn")]
        public PlayerActionList Turn { get; set; }

        /// <summary>
        /// Contains all PlayerAction's done by the player on the River;
        /// </summary>
        [JsonProperty("River")]
        public PlayerActionList River { get; set; }

        /// <summary>
        /// Contains List<PlayerAction> for each Street:
        ///     - PreFlop
        ///     - Flop
        ///     - Turn
        ///     - River;
        /// </summary>
        public AllActions()
        {
            PreFlop = new ();
            Flop = new ();
            Turn = new ();
            River = new ();       
        }
    }

    /// <summary>
    /// Contains all the info necessary to define an Action;
    /// </summary>
    public struct PlayerAction
    {
        /// <summary>
        /// Action. Could be:
        ///     - X = "checks"
        ///     - C = "calls"
        ///     - B = "bets"
        ///     - R = "raises"
        ///     - F = "folds";
        /// </summary>
        [JsonProperty("Act")]
        public string Act;

        /// <summary>
        /// Amount of Chips bet or called on the street if any; 
        /// </summary>
        [JsonProperty("Size")]
        public float Size;

        /// <summary>
        /// 0 = the Player is NOT all-in;
        /// 1 = the Player is all-in;
        /// </summary>
        [JsonProperty("AI")]
        public sbyte AI;

        /// <summary>
        /// Contains all the info necessary to define an Action;
        /// </summary>
        public PlayerAction()
        {
            Act = "";
            Size = 0;
            AI = 0;
        }

    }

    /// <summary>
    /// Contains all the specific Info for a single player in the Hand; Each player is described trough 1 SeatAction;
    /// </summary>
    public class SeatAction
    {
        /// <summary>
        /// Number of the Seat;
        /// </summary>
        public sbyte SeatNumber { get; set; }

        /// <summary>
        /// Amount of Chips before the start of the Hand;
        /// </summary>
        public float StartingStack { get; set; }

        /// <summary>
        /// Ante; It's a small amount of Chips << 1 BigBlind , in many tournaments it's 0;
        /// </summary>
        public float Ante { get; set; }

        /// <summary>
        /// Amount of Chips the player did invest as small or big Blind; If the Player isn't on the small/big blind would be 0;
        /// </summary>
        public float Blind { get; set; }

        /// <summary>
        /// Position of the player as int:
        ///     - 0 - BTN;
        ///     - 1 - CO;
        ///     - 2 - HJ;
        ///     - etc;
        ///     - 7 - UTG-1;
        ///     - 8 - SB;
        ///     - 9 - BB;
        /// </summary>
        public uint SeatPosition { get; set; }

        /// <summary>
        /// Street on which the player went all-in:
        ///     - "" - None;
        ///     - PF - PreFlop
        ///     - F  - Flop
        ///     - T  - Turn
        ///     - R  - River; 
        /// </summary>
        public string StreetAI { get; set; }

        /// <summary>
        /// Total amount of Chips invested of the player in the hand;
        /// </summary>
        public float ChipsInvested { get; set; }

        /// <summary>
        /// Total (real) amount of chips won by the player in the hand;
        /// </summary>
        public float ChipsWon { get; set; }

        /// <summary>
        /// Total (expected) amount of chips earned by the player in the hand;
        /// </summary>
        public float CevWon { get; set; }

        /// <summary>
        /// True = The player won the hand, FALSE = lost the hand;
        /// </summary>
        public bool IsWinner { get; set; }

        /// <summary>
        /// Contains all Actions done by the Player in the Hand;
        /// </summary>
        [JsonProperty("Actions")]
        public AllActions Actions { get; set; }

        /// <summary>
        /// 1.HoleCard;
        /// </summary>
        public Card HC1 { get; set; }

        /// <summary>
        /// 2.HoleCard;
        /// </summary>
        public Card HC2 { get; set; }

        /// <summary>
        /// The both HoleCards presented as Id(CardAllSimple Enum), used for displaying/quering HeatMaps and other HoleCards defined queries; 
        /// </summary>
        public int HCsAsNumber { get; set; }

        /// <summary>
        /// TRUE = The player did participate on the Flop; False = did fold/won the hand before the Flop;
        /// </summary>
        public bool saw_flop { get; set; }

        /// <summary>
        /// TRUE = The player did participate on the Turn; False = did fold/won the hand before the Turn;
        /// </summary>
        public bool saw_turn { get; set; }

        /// <summary>
        /// TRUE = The player did participate on the River; False = did fold/won the hand before the River;
        /// </summary>
        public bool saw_river { get; set; }

        /// <summary>
        /// TRUE = The player did reach ShowDown; False = did fold/won the hand before the ShowDown;
        /// </summary>
        public bool saw_showdown { get; set; }

        /// <summary>
        /// TRUE = if no other player did raise PreFlop before the current players 1. Action;
        /// FALSE = Atleast one other player did raise before the current players 1. Action;
        /// </summary>
        public bool pf_open_opp { get; set; }

        /// <summary>
        /// Contains all the specific Info for a single player in the Hand; Each player is described trough 1 SeatAction;
        /// </summary>
        public SeatAction()
        {
            Actions = new AllActions();
        }

    }
}

