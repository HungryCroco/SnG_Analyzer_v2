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

        [JsonProperty("cev")]
        public double Cev { get; set; }

        [JsonProperty("aBB")]
        public double Abb { get; set; }

        [JsonProperty("amt_won")]
        public double Amt_won { get; set; }

        [JsonProperty("aBI")]
        public double Abi { get; set; }

        [JsonProperty("count_tourneys")]
        public int Count_tourneys { get; set; }

        [JsonProperty("situations")]
        public int Situations { get; set; }

        [JsonProperty("t_date")]
        public DateTime Date { get; set; }


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

