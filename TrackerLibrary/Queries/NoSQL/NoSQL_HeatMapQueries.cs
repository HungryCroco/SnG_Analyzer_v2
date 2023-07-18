using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Queries.NoSQL
{
    /// <summary>
    /// Class containing all Queries necessary to request HeatMaps from NoSQL DB;
    /// </summary>
    public static  class NoSQL_HeatMapQueries
    {
		/// <summary>
		/// Get a JSON of StatsModel grouped by HoleCard's Ids;
		/// </summary>
		public static string NoSQL_ExportHeatMapAsJSON =
            @"SELECT array_to_json(array_agg(row_to_json(t2)))
			FROM (
				SELECT (ha->'SeatActions'->'@hero'->>'HCsAsNumber')::numeric AS hcs_id , COUNT(ha->'SeatActions'->'@hero'->>'HCsAsNumber')::numeric AS amt_situations
				FROM (
					SELECT data AS ha
					FROM hands h
					CROSS JOIN LATERAL jsonb_each(h.data->'SeatActions') AS t(k,v)
					WHERE @whereClauseVillain
					)  t1						
				WHERE @whereClauseHero
				GROUP BY (ha->'SeatActions'->'@hero'->>'HCsAsNumber')::numeric
				ORDER BY (ha->'SeatActions'->'@hero'->>'HCsAsNumber')::numeric 
				) t2";

        /// <summary>
        /// Get a DataGridView filtered by HoleCard's Id;
        /// </summary>
        public static string NoSQL_ExportDataGridViewByHoleCardsSimple =
            @"	SELECT ha->'Info'->>'Room' AS Room,
					ha->'Info'->>'HandIdBySite' AS handId,
					(ha->'Info'->>'BuyIn') || (ha->'Info'->>'Currency') || '+' || (ha->'Info'->>'Fee') || (ha->'Info'->>'Currency') AS BuyIn,
					'hero' AS hero,
					ha->'SeatActions'->'IPray2Buddha'->>'SeatPosition' AS h_Pos,
					(ha->'SeatActions'->'IPray2Buddha'->'HC1'->>'Name') || ' ' || (ha->'SeatActions'->'IPray2Buddha'->'HC2'->>'Name') AS h_HCs,
					ha->'SeatActions'->'IPray2Buddha'->'Actions'->>'PreFlop' AS h_PF_Action,
					ha->'SeatActions'->'IPray2Buddha'->'Actions'->>'Flop' AS h_F_Action,
					ha->'SeatActions'->'IPray2Buddha'->'Actions'->>'Turn' AS h_T_Action,
					ha->'SeatActions'->'IPray2Buddha'->'Actions'->>'River' AS h_R_Action,
					ha->'SeatActions'->'IPray2Buddha'->>'CevWon' AS h_cev_won,
					ha->'SeatActions'->'IPray2Buddha'->>'ChipsWon' AS h_amt_won,
		
		
					t1.villain AS villain,
					t1.villainPos AS v_Pos,
					t1.villainHCs AS v_HCs,
					t1.V_PF_Action AS v_PF_Action,
					t1.V_F_Action AS v_F_Action,
					t1.V_T_Action AS v_T_Action,
					t1.V_R_Action AS v_R_Action
				
				FROM (
					SELECT data AS ha, t.k AS villain, t.v->'SeatPosition' AS villainPos , 
						(t.v->'HC1'->>'Name') || ' ' || (t.v->'HC2'->>'Name') AS villainHCs,
					t.v->'Actions'->>'PreFlop' AS V_PF_Action,
					t.v->'Actions'->>'PreFlop' AS V_F_Action,
					t.v->'Actions'->>'PreFlop' AS V_T_Action,
					t.v->'Actions'->>'PreFlop' AS V_R_Action
					FROM hands h
					CROSS JOIN LATERAL jsonb_each(h.data->'SeatActions') AS t(k,v)
					WHERE @whereClauseVillain 
					)  t1						
				WHERE @whereClauseHero AND (ha->'SeatActions'->'IPray2Buddha'->>'HCsAsNumber')::numeric = @HCsId";

		/// <summary>
		/// Hero's where conditions for requesting BlindvsBlind Iso Stats;
		/// Needs to be concatenated either to HeatMap-StatsModel or -DataGridView Query;
		/// </summary>
        public static string NoSQL_WhereClauseHero_BvB_Iso =
            @"ha->'Info'->>'TournamentType' = '@tourneyType' 
				AND (ha->'SeatActions'->'@hero'->>'HCsAsNumber')::numeric > 0 
				AND (ha->'SeatActions'->'@hero'->>'SeatPosition')::numeric = 9 
				AND ((ha->'SeatActions'->'IPray2Buddha'->>'StartingStack')::numeric / (ha->'Info'->>'Amt_bb')::numeric) @es
				AND ha->'SeatActions'->'@hero'->'Actions'->'PreFlop'->0->>'Act' LIKE 'raises' 
				AND ((ha->'SeatActions'->'@hero'->'Actions'->'PreFlop'->0->>'Size')::numeric / (ha->'Info'->>'Amt_bb')::numeric) @size 
				AND (ha->'SeatActions'->'@hero'->'Actions'->'PreFlop'->0->>'AI')::numeric = @AI 
				AND (ha->'Info'->>'CntPlayers')::numeric = 3   AND (ha->'Info'->>'Date')::date > DATE '@date' 
				AND ha->'Info'->>'pf_actors' LIKE '89%' 
				AND ha->'Info'->>'pf_aggressors' LIKE '9%'";

        /// <summary>
        /// Villains's where conditions for requesting BlindvsBlind Iso Stats;
        /// Needs to be concatenated either to HeatMap-StatsModel or -DataGridView Query;
        /// </summary>
        public static string NoSQL_WhereClauseVillain_BvB_Iso =
            @"t.k @regList 
				AND t.v->'SeatPosition' = '8' 
				AND ((t.v->>'StartingStack')::numeric / (data->'Info'->>'Amt_bb')::numeric) @es";

        //-----

        /// <summary>
		/// Hero's where conditions for requesting BlindvsBlind openRaise Stats;
		/// Needs to be concatenated either to HeatMap-StatsModel or -DataGridView Query;
		/// </summary>
        public static string NoSQL_WhereClauseHero_BvB_oR =
            @"ha->'Info'->>'TournamentType' = '@tourneyType' 
				AND (ha->'SeatActions'->'@hero'->>'HCsAsNumber')::numeric > 0 
				AND (ha->'SeatActions'->'@hero'->>'SeatPosition')::numeric = 8  
				AND ((ha->'SeatActions'->'IPray2Buddha'->>'StartingStack')::numeric / (ha->'Info'->>'Amt_bb')::numeric) @es
				AND ha->'SeatActions'->'@hero'->'Actions'->'PreFlop'->0->>'Act' LIKE 'raises' 
				AND ((ha->'SeatActions'->'@hero'->'Actions'->'PreFlop'->0->>'Size')::numeric / (ha->'Info'->>'Amt_bb')::numeric) @size 
				AND (ha->'SeatActions'->'@hero'->'Actions'->'PreFlop'->0->>'AI')::numeric = @AI 
				AND (ha->'Info'->>'CntPlayers')::numeric = 3   
				AND (ha->'Info'->>'Date')::date > DATE '@date' 
				AND ha->'Info'->>'pf_actors' LIKE '89%' 
				AND ha->'Info'->>'pf_aggressors' LIKE '8%'";

        /// <summary>
        /// Villain's where conditions for requesting BlindvsBlind openRaise Stats;
        /// Needs to be concatenated either to HeatMap-StatsModel or -DataGridView Query;
        /// </summary>
        public static string NoSQL_WhereClauseVillain_BvB_oR =
            @"t.k @regList 
				AND t.v->'SeatPosition' = '9' 
				AND ((t.v->>'StartingStack')::numeric / (data->'Info'->>'Amt_bb')::numeric) @es";

        //-----

        /// <summary>
        /// Hero's where conditions for requesting BlindvsBlind openLimp Stats;
        /// Needs to be concatenated either to HeatMap-StatsModel or -DataGridView Query;
        /// </summary>
        public static string NoSQL_WhereClauseHero_BvB_oL =
            @"ha->'Info'->>'TournamentType' = '@tourneyType' 
				AND (ha->'SeatActions'->'@hero'->>'HCsAsNumber')::numeric > 0 
				AND (ha->'SeatActions'->'@hero'->>'SeatPosition')::numeric = 8  
				AND ((ha->'SeatActions'->'IPray2Buddha'->>'StartingStack')::numeric / (ha->'Info'->>'Amt_bb')::numeric) @es
				AND ha->'SeatActions'->'@hero'->'Actions'->'PreFlop'->0->>'Act' LIKE 'calls' 
				AND (ha->'Info'->>'CntPlayers')::numeric = 3   AND (ha->'Info'->>'Date')::date > DATE '@date' 
				AND ha->'Info'->>'pf_actors' LIKE '8%'";

        /// <summary>
        /// Villain's where conditions for requesting BlindvsBlind openLimp Stats;
        /// Needs to be concatenated either to HeatMap-StatsModel or -DataGridView Query;
        /// </summary>
        public static string NoSQL_WhereClauseVillain_BvB_oL =
            @"t.k @regList 
				AND t.v->'SeatPosition' = '9' 
				AND ((t.v->>'StartingStack')::numeric / (data->'Info'->>'Amt_bb')::numeric) @es";

    }
}
