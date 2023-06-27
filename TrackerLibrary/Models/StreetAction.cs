using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    public class StreetAction
    {
        public struct FullAction
        {
            [JsonProperty("Player")]
            public string Player;

            [JsonProperty("Act")]
            public string Act;

            [JsonProperty("Size")]
            public float Size;

            [JsonProperty("Act")]
            public sbyte AI;

        }

        public List<FullAction> PreflopActions { get; set; }

        public List<FullAction> FlopActions { get; set; }

        public List<FullAction> TurnActions { get; set; }

        public List<FullAction> RiverActions { get; set; }

        public StreetAction()
        {
            PreflopActions = new List<FullAction>();
            FlopActions = new List<FullAction>();
            TurnActions = new List<FullAction>();
            RiverActions = new List<FullAction>();
        }

    }
}
