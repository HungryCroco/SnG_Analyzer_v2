using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Queries.SQL
{
    public static class SQL_DashBoardQueries
    {
        public static string sql_ExportCevPerTournamentAsJSON =
            @"SELECT array_to_json(array_agg(row_to_json(t)))
			FROM (
				SELECT SUM(sa.cev_won) AS cev, SUM(sa.chips_won) AS amt_won
				FROM public.tournament ta
								INNER JOIN public.hands ha ON ha.tournamentid=ta.id
								INNER JOIN public.seataction sa ON sa.Id=ha.heroseatactionid
								INNER JOIN public.player pl ON (sa.playerId = pl.id)

				WHERE ta.tournamenttype ='@tourneyType' AND pl.playernickname = '@hero'
				GROUP BY ta.id 
				ORDER BY ta.tournamentidbysite
				) t";

		public static string sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths = @"
			SELECT array_to_json(array_agg(row_to_json(t2)))
			FROM (
				SELECT COUNT(DISTINCT t1.tourney_id)::numeric AS Count_tourneys, COUNT(t1.aBB) AS Situations, SUM(t1.cev_won) AS Cev, SUM(t1.chips_won) AS Amt_won, SUM (t1.aBB) AS Abb, to_char(MIN(t1.t_date),'YYYY-MM-DD') AS T_Date
				FROM (
					SELECT (sa.cev_won / ha.amt_bb) AS aBB, ha.handid AS hid, sa.cev_won AS cev_won, sa.chips_won AS chips_won, ta.tournamentdate AS t_date, ta.TournamentIdBySite AS tourney_id
					FROM public.hands ha
						INNER JOIN public.tournament ta ON ta.id = ha.tournamentid
						INNER JOIN public.seataction sa ON ha.heroseatactionid = sa.id
					WHERE ta.tournamenttype = '@tourneyType' AND 
						sa.position = '@posHero' AND 
						ha.handid IN 
							(SELECT h.handid FROM public.hands h
											INNER JOIN public.tournament ta ON ta.id = h.tournamentid
											INNER JOIN public.seataction sa ON sa.Id=h.@seatActionVillain
											INNER JOIN public.player villian ON villian.id = sa.playerid 
							WHERE --h.btnSeatActionId @btnNULL AND
								h.cnt_players = @cntPlayers
								--AND (h.pf_actors @pfActors )
								AND (COALESCE(h.pf_actors, '') @pfActors)
				 				AND villian.playernickname @regList
							) 
					GROUP BY hid, aBB, t_date, cev_won, chips_won, tourney_id
					) t1
				GROUP BY DATE_TRUNC('month',t_date)
				ORDER BY DATE_TRUNC('month',t_date) DESC
				) t2";

        public static string sql_ExportTlpOverviewAsJSON_CevTotal_GroupByMonths = @"
				SELECT array_to_json(array_agg(row_to_json(t2)))
				FROM (
 						SELECT sum (t1.cev) as Cev , sum(t1.Chips_Won) as Amt_won, avg(t1.aBI) as Abi, count(t1.cev) as Count_Tourneys, to_char(min(t1.t_date),'YYYY-MM-DD') as T_Date
							FROM ( 
									SELECT sum (sa.cev_won) as CEV , sum(sa.chips_won) as Chips_Won , ta.amt_buyin as aBI , ta.tournamentdate as t_date
  									FROM public.tournament ta
										inner join public.hands ha on ha.tournamentid=ta.id
										inner join public.seataction sa on sa.Id=ha.heroseatactionid
										inner join public.player pa on (sa.playerId = pa.id)
										
									WHERE pa.playernickname = '@hero' and ha.tournamenttype = '@tourneyType' 
									GROUP BY ta.id
									ORDER BY ta.id
								) t1
				GROUP BY DATE_TRUNC('month',t_date)
				ORDER BY DATE_TRUNC('month',t_date) DESC
					) t2";

    }
}
