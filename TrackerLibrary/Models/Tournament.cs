using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    public class Tournament
    {
        public TournamentInfo Info { get; set; }

        public List<Hand> Hands { get; set; }



        public Tournament()
        {
            Info = new TournamentInfo();
            Hands = new List<Hand>();
        }


    }

    
}

