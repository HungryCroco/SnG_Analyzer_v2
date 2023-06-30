using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Queries.NoSQL;
using TrackerLibrary.Queries;

namespace TrackerLibrary.Models
{
    public class DashBoardModel
    {
        public List<CevModel> CevModel_Total_ByTournament { get; set; }

        public List<CevModel> CevModel_Total_ByMonth { get; set; }

        public List<CevModel> CevModel_3W_BTNvBBvREG { get; set; }

        public List<CevModel> CevModel_3W_BTNvBBvFISH { get; set; }

        public List<CevModel> CevModel_3W_SBvBTNvREG { get; set; }

        public List<CevModel> CevModel_3W_SBvBTNvFISH { get; set; }

        public List<CevModel> CevModel_3W_BBvBTNvREG { get; set; }

        public List<CevModel> CevModel_3W_BBvBTNvFISH { get; set; }

        public List<CevModel> CevModel_3W_SBvBBvREG { get; set; }

        public List<CevModel> CevModel_3W_SBvBBvFISH { get; set; }

        public List<CevModel> CevModel_3W_BBvSBvREG { get; set; }

        public List<CevModel> CevModel_3W_BBvSBvFISH { get; set; }

        public List<CevModel> CevModel_HU_SBvREG { get; set; }

        public List<CevModel> CevModel_HU_SBvFISH { get; set; }

        public List<CevModel> CevModel_HU_BBvREG { get; set; }

        public List<CevModel> CevModel_HU_BBvFISH { get; set; }





    }
}
