using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    /// <summary>
    /// This Class is used to power up JSON-Serialized Queries grouped by HoleCards; 
    /// </summary>
    [Serializable]
    public class StatsModel
    {
        /// <summary>
        /// HoleCards Id (Enum CardAllSimple);
        /// </summary>
        [JsonProperty("hcs_id")]
        public int CardId { get; set; }

        /// <summary>
        /// Amount of cases;
        /// </summary>
        [JsonProperty("amt_situations")]
        public int Situations { get; set; }


        //public StatsModel()
        //{
        //}

    }
}

