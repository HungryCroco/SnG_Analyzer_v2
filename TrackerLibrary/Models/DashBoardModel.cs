using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Queries.NoSQL;
using TrackerLibrary.Queries;

namespace TrackerLibrary.Models
{
    /// <summary>
    /// A DashBoardModel contains all the necessary data to power up a Dashboard UI-Form; 
    /// </summary>
    public class DashBoardModel
    {
        /// <summary>
        /// CevModel requested by tournament;
        /// </summary>
        public List<CevModel> CevModel_Total_ByTournament { get; set; }

        /// <summary>
        /// CevModel requested by month;
        /// </summary>
        public List<CevModel> CevModel_Total_ByMonth { get; set; }

        /// <summary>
        /// CevModel requested by month, filtered out only cases when:
        ///  - 3+ players in the hand;
        ///  - hero is BTN;
        ///  - villain is BB AND in the red List;
        /// </summary>
        public List<CevModel> CevModel_3W_BTNvBBvREG { get; set; }

        /// <summary>
        /// CevModel requested by month, filtered out only cases when:
        ///  - 3+ players in the hand;
        ///  - hero is BTN;
        ///  - villain is BB AND NOT in the red List;
        /// </summary>
        public List<CevModel> CevModel_3W_BTNvBBvFISH { get; set; }

        /// <summary>
        /// CevModel requested by month, filtered out only cases when:
        ///  - 3+ players in the hand;
        ///  - hero is SB;
        ///  - villain is BTN AND in the red List;
        /// </summary>
        public List<CevModel> CevModel_3W_SBvBTNvREG { get; set; }

        /// <summary>
        /// CevModel requested by month, filtered out only cases when:
        ///  - 3+ players in the hand;
        ///  - hero is SB;
        ///  - villain is BTN AND NOT in the red List;
        /// </summary>
        public List<CevModel> CevModel_3W_SBvBTNvFISH { get; set; }

        /// <summary>
        /// CevModel requested by month, filtered out only cases when:
        ///  - 3+ players in the hand;
        ///  - hero is BB;
        ///  - villain is BTN AND in the red List;
        /// </summary>
        public List<CevModel> CevModel_3W_BBvBTNvREG { get; set; }

        /// <summary>
        /// CevModel requested by month, filtered out only cases when:
        ///  - 3+ players in the hand;
        ///  - hero is BB;
        ///  - villain is BTN AND NOT in the red List;
        /// </summary>
        public List<CevModel> CevModel_3W_BBvBTNvFISH { get; set; }

        /// <summary>
        /// CevModel requested by month, filtered out only cases when:
        ///  - 3+ players in the hand 
        ///  - Blind vs Blind;
        ///  - hero is SB;
        ///  - villain is BB AND in the red List;
        /// </summary>
        public List<CevModel> CevModel_3W_SBvBBvREG { get; set; }

        /// <summary>
        /// CevModel requested by month, filtered out only cases when:
        ///  - 3+ players in the hand 
        ///  - Blind vs Blind;
        ///  - hero is SB;
        ///  - villain is BB AND NOT in the red List;
        /// </summary>
        public List<CevModel> CevModel_3W_SBvBBvFISH { get; set; }

        /// <summary>
        /// CevModel requested by month, filtered out only cases when:
        ///  - 3+ players in the hand 
        ///  - Blind vs Blind;
        ///  - hero is BB;
        ///  - villain is SB AND in the red List;
        /// </summary>
        public List<CevModel> CevModel_3W_BBvSBvREG { get; set; }

        /// <summary>
        /// CevModel requested by month, filtered out only cases when:
        ///  - 3+ players in the hand 
        ///  - Blind vs Blind;
        ///  - hero is BB;
        ///  - villain is SB AND NOT in the red List;
        /// </summary>
        public List<CevModel> CevModel_3W_BBvSBvFISH { get; set; }

        /// <summary>
        /// CevModel requested by month, filtered out only cases when:
        ///  - 2 players in the hand 
        ///  - hero is SB;
        ///  - villain is BB AND in the red List;
        /// </summary>
        public List<CevModel> CevModel_HU_SBvREG { get; set; }

        /// <summary>
        /// CevModel requested by month, filtered out only cases when:
        ///  - 2 players in the hand 
        ///  - hero is SB;
        ///  - villain is BB AND NOT in the red List;
        /// </summary>
        public List<CevModel> CevModel_HU_SBvFISH { get; set; }

        /// <summary>
        /// CevModel requested by month, filtered out only cases when:
        ///  - 2 players in the hand 
        ///  - hero is BB;
        ///  - villain is SB AND in the red List;
        /// </summary>
        public List<CevModel> CevModel_HU_BBvREG { get; set; }

        /// <summary>
        /// CevModel requested by month, filtered out only cases when:
        ///  - 2 players in the hand 
        ///  - hero is BB;
        ///  - villain is SB AND NOT in the red List;
        /// </summary>
        public List<CevModel> CevModel_HU_BBvFISH { get; set; }





    }
}
