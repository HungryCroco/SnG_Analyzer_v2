using Newtonsoft.Json;


namespace TrackerLibrary.Models
{
    /// <summary>
    /// A CevModel contains all the necessary data for requesting a query from SQL/NoSQL databases;
    /// </summary>
    [Serializable]
    public class CevModel
    {
        /// <summary>
        /// Expected Value(adjusted Chips won) as Chips;
        /// </summary>
        [JsonProperty("Cev")]
        public double Cev { get; set; }

        /// <summary>
        /// Expected bb/hand;
        /// </summary>
        [JsonProperty("Abb")]
        public double Abb { get; set; }

        /// <summary>
        /// Actual/Real Chips won;
        /// </summary>
        [JsonProperty("Amt_won")]
        public double Amt_won { get; set; }

        /// <summary>
        /// BuyIn
        /// </summary>
        [JsonProperty("Abi")]
        public double Abi { get; set; }

        /// <summary>
        /// Number of Tournaments
        /// </summary>
        [JsonProperty("Count_tourneys")]
        public int Count_tourneys { get; set; }

        /// <summary>
        /// Number of Hands
        /// </summary>
        [JsonProperty("Situations")]
        public int Situations { get; set; }

        /// <summary>
        /// Date
        /// </summary>
        [JsonProperty("T_Date")]
        public DateTime T_Date { get; set; }


        //public CevModel()
        //{
        //}


    }
}

