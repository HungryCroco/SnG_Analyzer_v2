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
            public string Player;
            public string Act;
            public float Size;
            //public float AmtBefore;
            //public float ChipsInvested;
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
