using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Queries.NoSQL
{
    public static  class HeatMapQueries
    {
		public static string sql_ExportHeatMapAsJSON_BvB_Iso =
            @"SELECT array_to_json(array_agg(row_to_json(t2)))
				FROM (
					SELECT (ha->'SeatActions'->'@hero'->>'HCsAsNumber')::numeric AS hcs_id , COUNT(ha->'SeatActions'->'@hero'->>'HCsAsNumber')::numeric AS amt_situations
						FROM (
							SELECT data AS ha
							FROM hands h
							CROSS JOIN LATERAL jsonb_each(h.data->'SeatActions') AS t(k,v)
							WHERE t.k @regList AND t.v->'SeatPosition' = '8' 
							)  t1						
						WHERE ha->'Info'->>'TournamentType' = '@tourneyType' AND (ha->'SeatActions'->'@hero'->>'HCsAsNumber')::numeric > 0 AND (ha->'SeatActions'->'@hero'->>'SeatPosition')::numeric = 9  AND 
							ha->'SeatActions'->'@hero'->'Actions'->'PreFlop'->0->>'Act' LIKE 'raises' AND ((ha->'SeatActions'->'@hero'->'Actions'->'PreFlop'->0->>'Size')::numeric / (ha->'Info'->>'Amt_bb')::numeric) @size AND
							(ha->'SeatActions'->'@hero'->'Actions'->'PreFlop'->0->>'AI')::numeric = @AI AND (ha->'Info'->>'CntPlayers')::numeric = 3   AND (ha->'Info'->>'Date')::date > DATE '@date' AND
							ha->'Info'->>'pf_actors' LIKE '89%' AND ha->'Info'->>'pf_aggressors' LIKE '9%'
						GROUP BY (ha->'SeatActions'->'@hero'->>'HCsAsNumber')::numeric
						ORDER BY (ha->'SeatActions'->'@hero'->>'HCsAsNumber')::numeric 
					) t2";


    //        @"SELECT array_to_json(array_agg(row_to_json(t2)))
				//FROM (
				//	SELECT (ha->'SeatActions'->'IPray2Buddha'->>'HCsAsNumber')::numeric AS HCsId , COUNT(ha->'SeatActions'->'IPray2Buddha'->>'HCsAsNumber')::numeric AS HCs
				//		FROM (
				//			SELECT data AS ha
				//			FROM hands h
				//			CROSS JOIN LATERAL jsonb_each(h.data->'SeatActions') AS t(k,v)
				//			WHERE t.k NOT IN ('aspirine911', 'gRRRiz', 'Tommce18', 'mularczykPVC', 'BnG18', 'SolFlava', 'mildzsuOO') AND t.v->'SeatPosition' = '8' 
				//			)  t1						
				//		WHERE ha->'Info'->>'TournamentType' = '3-max' AND (ha->'SeatActions'->'IPray2Buddha'->>'HCsAsNumber')::numeric > 0 AND (ha->'SeatActions'->'IPray2Buddha'->>'SeatPosition')::numeric = 9  AND 
				//			ha->'SeatActions'->'IPray2Buddha'->'Actions'->'PreFlop'->0->>'Act' LIKE 'raises' AND ((ha->'SeatActions'->'IPray2Buddha'->'Actions'->'PreFlop'->0->>'Size')::numeric / (ha->'Info'->>'Amt_bb')::numeric) BETWEEN 2 AND 4 AND
				//			(ha->'SeatActions'->'IPray2Buddha'->'Actions'->'PreFlop'->0->>'AI')::numeric = 0 AND (ha->'Info'->>'CntPlayers')::numeric = 3   AND (ha->'Info'->>'Date')::date > DATE '1999-03-01' AND
				//			ha->'Info'->>'pf_actors' LIKE '89%' AND ha->'Info'->>'pf_aggressors' LIKE '9%'
				//		GROUP BY (ha->'SeatActions'->'IPray2Buddha'->>'HCsAsNumber')::numeric
				//		ORDER BY (ha->'SeatActions'->'IPray2Buddha'->>'HCsAsNumber')::numeric 
				//	) t2";

    }
}
