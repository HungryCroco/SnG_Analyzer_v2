using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;


namespace TrackerLibrary.Models
{
    public class Hand
    {
        [JsonProperty("info")]
        public HandInfo Info { get; set; }

        [JsonProperty("streetAction")]
        public StreetAction StreetActions { get; set; }

        [JsonProperty("seatAction")]
        public Dictionary<string,SeatAction> SeatActions { get; set; }

        //public Dictionary<string, SeatAction> SeatActions { get; set; }

        public Hand()
        {
            StreetActions = new();
            SeatActions = new();
            Info = new HandInfo();

        }

        public Hand( HandInfo handInfo, StreetAction streetActions ,Dictionary<string, SeatAction> seatActions)
        {
            Info = handInfo;
            StreetActions = streetActions;
            SeatActions = seatActions;
            
        }


        public override bool Equals(object obj) // override to distinguish same Hands by 'Room' and 'HandIdBySite';
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Hand other = (Hand)obj;
            return Info.Room == other.Info.Room && Info.HandIdBySite == other.Info.HandIdBySite;
        }

        public override int GetHashCode() // override to distinguish same Hands by 'Room' and 'HandIdBySite';
        {
            return HashCode.Combine(Info.Room, Info.HandIdBySite);
        }

    }
}
