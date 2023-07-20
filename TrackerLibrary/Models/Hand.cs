using Newtonsoft.Json;


namespace TrackerLibrary.Models
{
    /// <summary>
    /// Containes all the information defined by a Hand;
    /// </summary>
    public class Hand
    {
        /// <summary>
        /// Contains all Properties, that are defined by the Hand and are the same for all Players;
        /// </summary>
        [JsonProperty("info")]
        public HandInfo Info { get; set; }

        /// <summary>
        /// Contains all player's actions parted by Street;
        /// </summary>
        [JsonProperty("streetAction")]
        public StreetAction StreetActions { get; set; }

        /// <summary>
        /// Dict(playerNickName,SeatAction); Contains all player's actions parted by Player;
        /// </summary>
        [JsonProperty("seatAction")]
        public Dictionary<string,SeatAction> SeatActions { get; set; }

        /// <summary>
        /// Containes all the information defined by a Hand;
        /// </summary>
        public Hand()
        {
            StreetActions = new();
            SeatActions = new();
            Info = new HandInfo();

        }

        /// <summary>
        /// Containes all the information defined by a Hand;
        /// </summary>
        /// <param name="handInfo">Contains all Properties, that are defined by the Hand and are the same for all Players;</param>
        /// <param name="streetActions">Contains all player's actions parted by Street;</param>
        /// <param name="seatActions">Dict(playerNickName,SeatAction); Contains all player's actions parted by Player;</param>
        public Hand( HandInfo handInfo, StreetAction streetActions ,Dictionary<string, SeatAction> seatActions)
        {
            Info = handInfo;
            StreetActions = streetActions;
            SeatActions = seatActions;
            
        }


        //public override bool Equals(object obj) // override to distinguish same Hands by 'Room' and 'HandIdBySite';
        //{
        //    if (obj == null || GetType() != obj.GetType())
        //        return false;

        //    Hand other = (Hand)obj;
        //    return Info.Room == other.Info.Room && Info.HandIdBySite == other.Info.HandIdBySite;
        //}

        //public override int GetHashCode() // override to distinguish same Hands by 'Room' and 'HandIdBySite';
        //{
        //    return HashCode.Combine(Info.Room, Info.HandIdBySite);
        //}

    }
}
