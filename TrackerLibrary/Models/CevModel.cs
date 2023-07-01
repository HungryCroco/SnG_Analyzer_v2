using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    [Serializable]
    public class CevModel
    {
        //[JsonProperty("num")]
        //public int CardId { get; set; }

        [JsonProperty("Cev")]
        public double Cev { get; set; }

        [JsonProperty("Abb")]
        public double Abb { get; set; }

        [JsonProperty("Amt_won")]
        public double Amt_won { get; set; }

        [JsonProperty("Abi")]
        public double Abi { get; set; }

        [JsonProperty("Count_tourneys")]
        public int Count_tourneys { get; set; }

        [JsonProperty("Situations")]
        public int Situations { get; set; }

        [JsonProperty("T_Date")]
        public DateTime T_Date { get; set; }


        public CevModel()
        {
        }

        //public StatsModel(string sqlQuery, string name)
        //{
        //    SqlQuery = sqlQuery;
        //    Name = name;
        //}
    }
}

