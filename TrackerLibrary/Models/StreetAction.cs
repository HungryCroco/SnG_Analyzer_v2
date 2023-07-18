using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    /// <summary>
    /// This Class is used to save all the Actions ordered by Street;
    /// </summary>
    public class StreetAction
    {
        /// <summary>
        /// It's defining an Action; A list of FullAction defines a StreetAction
        /// </summary>
        public struct FullAction
        {
            /// <summary>
            /// PlayerNickName of the active Player( that did the action);
            /// </summary>
            [JsonProperty("Player")]
            public string Player;

            /// <summary>
            /// Definition of the Action:
            ///     - "checks"
            ///     - "calls"
            ///     - "bets"
            ///     - "raises"
            ///     - "folds"
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
            [JsonProperty("Act")]
            public sbyte AI;

        }

        /// <summary>
        /// Contains all Actions did by all players PreFlop;
        /// </summary>
        public List<FullAction> PreflopActions { get; set; }

        /// <summary>
        /// Contains all Actions did by all players on the Flop;
        /// </summary>
        public List<FullAction> FlopActions { get; set; }

        /// <summary>
        /// Contains all Actions did by all players on the Turn;
        /// </summary>
        public List<FullAction> TurnActions { get; set; }

        /// <summary>
        /// Contains all Actions did by all players on the River;
        /// </summary>
        public List<FullAction> RiverActions { get; set; }

        /// <summary>
        /// Contains all Actions grouped by Street;
        /// </summary>
        public StreetAction()
        {
            PreflopActions = new List<FullAction>();
            FlopActions = new List<FullAction>();
            TurnActions = new List<FullAction>();
            RiverActions = new List<FullAction>();
        }

    }
}
