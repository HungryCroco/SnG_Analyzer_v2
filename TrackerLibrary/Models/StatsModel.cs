using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    [Serializable]
    public class StatsModel
    {
        [JsonProperty("hcs_id")]
        public int CardId { get; set; }

        [JsonProperty("amt_situations")]
        public int Situations { get; set; }


        public StatsModel()
        {
        }

    }
}

