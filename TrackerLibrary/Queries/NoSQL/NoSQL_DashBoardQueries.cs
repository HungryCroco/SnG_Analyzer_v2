using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Queries.NoSQL
{
    public static class NoSQL_DashBoardQueries
    {
        public static string sql_ExportCevPerTournamentAsJSON =
            @"SELECT array_to_json(array_agg(row_to_json(t2)))
				FROM (
					SELECT MAX(tourney_id) as tourney_id, SUM(t1.cev_won) as Cev, SUM(t1.chips_won) as Amt_won, SUM(t1.cev_won / t1.amt_bb) AS Abb, MAX(t1.t_date) AS T_Date 
						FROM (
							SELECT data->'Info'->>'TournamentIdBySite' AS tourney_id, (data->'SeatActions'->'@hero'->'CevWon')::numeric AS cev_won, (data->'SeatActions'->'@hero'->'ChipsWon')::numeric AS chips_won,
								(data->'Info'->'Amt_bb')::numeric AS amt_bb, TO_DATE((data->'Info'->>'Date')::text, 'YYYY-MM-DD') AS t_date	
									FROM hands ha
									WHERE data->'Info'->>'TournamentType' = '@tourneyType' AND data->'SeatActions'->'@hero' IS NOT NULL
							) t1
				GROUP BY tourney_id
				ORDER BY tourney_id ASC
					) t2";

        public static string sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths =
            @"SELECT array_to_json(array_agg(row_to_json(t3)))
				FROM (
					SELECT COUNT(DISTINCT t2.tourney_id)::numeric AS Count_tourneys, COUNT(t2.cev_won)::numeric AS Situations, SUM(t2.cev_won) AS Cev, SUM(t2.chips_won) as Amt_won, SUM(t2.cev_won / t2.amt_bb) AS Abb, MAX(t2.t_date) AS T_Date 
						FROM (
							SELECT ha->'Info'->>'TournamentIdBySite' AS tourney_id, (ha->'SeatActions'->'@hero'->'CevWon')::numeric AS cev_won, (ha->'SeatActions'->'@hero'->'ChipsWon')::numeric AS chips_won,
									(ha->'Info'->'Amt_bb')::numeric AS amt_bb, TO_DATE((ha->'Info'->>'Date')::text, 'YYYY-MM-DD') AS t_date	
										FROM (
												SELECT data AS ha
												FROM hands h
												CROSS JOIN LATERAL jsonb_each(h.data->'SeatActions') AS t(k,v)
												WHERE t.k @regList AND t.v->'SeatPosition' = '@posVillain')  t1
							WHERE ha->'SeatActions'->'@hero'->'SeatPosition' = '@posHero' AND 
								ha->'Info'->> 'CntPlayers' = '@cntPlayers' AND
								--ha->'Info'->> 'pf_actors' @pfActors
								(COALESCE(ha->'Info'->> 'pf_actors', '') @pfActors)
							GROUP BY tourney_id, t1.ha
							ORDER BY tourney_id 
							) t2
						GROUP BY DATE_TRUNC('month',t2.t_date)
						ORDER BY DATE_TRUNC('month',t2.t_date) DESC 
					) t3";

		public static string sql_ExportTlpOverviewAsJSON_CevTotal_GroupByMonths =
            @"SELECT array_to_json(array_agg(row_to_json(t3)))
				FROM (
					SELECT COUNT(DISTINCT t2.tourney_id)::numeric AS Count_tourneys, COUNT(t2.cev_won)::numeric AS Situations, SUM(t2.cev_won) AS Cev, SUM(t2.chips_won) as Amt_won, avg(t2.aBI) as Abi, SUM(t2.cev_won / t2.amt_bb) AS Abb, MAX(t2.t_date) AS T_Date 
						FROM (
							SELECT data->'Info'->>'TournamentIdBySite' AS tourney_id, (data->'SeatActions'->'@hero'->'CevWon')::numeric AS cev_won, (data->'SeatActions'->'@hero'->'ChipsWon')::numeric AS chips_won,
								(data->'Info'->>'BuyIn')::numeric AS aBI, (data->'Info'->'Amt_bb')::numeric AS amt_bb, TO_DATE((data->'Info'->>'Date')::text, 'YYYY-MM-DD') AS t_date	
										FROM hands
							WHERE data->'Info'->>'TournamentType' = '@tourneyType' @whereClauseQuery
							GROUP BY tourney_id, hands.data
							ORDER BY tourney_id 
							) t2
						GROUP BY DATE_TRUNC('month',t2.t_date)
						ORDER BY DATE_TRUNC('month',t2.t_date) DESC 
					) t3";

//        public static string sql_ExportTlpOverviewAsJSON_GeneralInfo_GroupByBlocks =
//            @"	
//set join_collapse_limit = 1;
//SELECT array_to_json(array_agg(row_to_json(t2)))
//				FROM (
// 						SELECT sum (t1.cev) as CEV , sum(t1.Chips_Won) as Amt_Won, avg(t1.aBI) as aBI, to_char(min(t1.t_date),'YYYY-MM-DD') as min_Date, to_char(max(t1.t_date),'YYYY-MM-DD') as max_Date
//							FROM ( 
//									SELECT sum (sa.cev_won) as CEV , sum(sa.chips_won) as Chips_Won , ta.amt_buyin as aBI , ta.tournamentdate as t_date, ROW_NUMBER() OVER(ORDER BY ta.id) rn
//  									FROM public.tournament ta
//										inner join public.hands ha on ha.tournamentid=ta.id
//										inner join public.seataction sa on (sa.Id=ha.seat1ActionId OR sa.Id=ha.seat2ActionId OR sa.Id=ha.seat3ActionId)
//										inner join public.player pa on (sa.playerId = pa.id)
										
//									WHERE pa.playernickname = 'IPray2Buddha' and ha.tournamenttype = '3-max' 
//									GROUP BY ta.id
//									ORDER BY ta.id
//								) t1
//						GROUP BY (rn - 1)/200
//						ORDER BY (rn - 1)/200
//					) t2";

//        public static string sql_ExportTlpOverviewAsJSON_GeneralInfo_GroupByMonths =
//            @"	
//set join_collapse_limit = 1;
//SELECT array_to_json(array_agg(row_to_json(t2)))
//				FROM (
// 						SELECT sum (t1.cev) as CEV , sum(t1.Chips_Won) as Amt_Won, avg(t1.aBI) as aBI, count(t1.cev) as Count_Tourney, to_char(min(t1.t_date),'YYYY-MM-DD') as min_Date, to_char(max(t1.t_date),'YYYY-MM-DD') as max_Date
//							FROM ( 
//									SELECT sum (sa.cev_won) as CEV , sum(sa.chips_won) as Chips_Won , ta.amt_buyin as aBI , ta.tournamentdate as t_date
//  									FROM public.tournament ta
//										inner join public.hands ha on ha.tournamentid=ta.id
//										inner join public.seataction sa on sa.Id=ha.heroseatactionid
//										inner join public.player pa on (sa.playerId = pa.id)
										
//									WHERE pa.playernickname = 'IPray2Buddha' and ha.tournamenttype = '3-max' 
//									GROUP BY ta.id
//									ORDER BY ta.id
//								) t1
//				GROUP BY DATE_TRUNC('month',t_date)
//				ORDER BY DATE_TRUNC('month',t_date) DESC
//					) t2";


     //   public static string sql_cevRequestGeneral=
     //       @"where (pl.PlayerNickName = @activePlayer::varchar AND ha.tournamenttype = @tourneyType::Varchar 
					//AND ta.amt_buyin = @amtBI::numeric)";

        public static string sql_cevRequestGeneral_test =
            @"where (pl.PlayerNickName = @activePlayer::varchar AND ha.tournamenttype = @tourneyType::Varchar)";
    }
}
