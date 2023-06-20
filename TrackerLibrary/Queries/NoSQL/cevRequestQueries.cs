using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Queries.NoSQL
{
    public static class cevRequestQueries
    {
        public static string sql_ExportCevPerTournamentAsJSON =
            @"
set join_collapse_limit = 1;
select array_to_json(array_agg(row_to_json(t)))
			from (

					select  sum (sa.cev_won) as cev , sum(sa.chips_won) as amt_won
						FROM public.tournament ta
										inner join public.hands ha on ha.tournamentid=ta.id
										inner join public.seataction sa on sa.Id=ha.heroseatactionid
										inner join public.player pl on (sa.playerId = pl.id)
										
						@whereClause
						group by ta.id order by ta.tournamentidbysite

			) t";

        public static string sql_ExportTlpOverviewAsJSON_CevByPos_GroupByMonths =
            @"
SELECT array_to_json(array_agg(row_to_json(t2)))
				FROM (SELECT sum (t1.aBB) as aBB , count(t1.aBB) as situations, to_char(min(t1.t_date),'YYYY-MM-DD') as min_Date, to_char(max(t1.t_date),'YYYY-MM-DD') as max_Date
							FROM (
								SELECT (sa.cev_won / ha.amt_bb) as aBB,  ta.tournamentdate as t_date, ha.handid as hid
										FROM public.hands ha
														inner join public.tournament ta ON ta.id = ha.tournamentid
														inner join public.seataction sa on ha.heroseatactionid = sa.id

										where ta.tournamenttype = @tourneyType::Varchar  AND sa.position = @pos::Varchar and ha.handid in 
												(select h.handid FROM public.hands h
																inner join public.tournament ta ON ta.id = h.tournamentid
																inner join public.seataction sa on sa.Id=h.@villianSAID
																inner join public.player villian ON villian.id = sa.playerid 
												where @HU_3W_Filter villian.playernickname @regList
												) 
										GROUP BY hid, aBB, t_date
								) t1
					GROUP BY DATE_TRUNC('month',t_date)
					ORDER BY DATE_TRUNC('month',t_date) DESC
					) t2";

        public static string sql_ExportTlpOverviewAsJSON_GeneralInfo_GroupByBlocks =
            @"	
set join_collapse_limit = 1;
SELECT array_to_json(array_agg(row_to_json(t2)))
				FROM (
 						SELECT sum (t1.cev) as CEV , sum(t1.Chips_Won) as Amt_Won, avg(t1.aBI) as aBI, to_char(min(t1.t_date),'YYYY-MM-DD') as min_Date, to_char(max(t1.t_date),'YYYY-MM-DD') as max_Date
							FROM ( 
									SELECT sum (sa.cev_won) as CEV , sum(sa.chips_won) as Chips_Won , ta.amt_buyin as aBI , ta.tournamentdate as t_date, ROW_NUMBER() OVER(ORDER BY ta.id) rn
  									FROM public.tournament ta
										inner join public.hands ha on ha.tournamentid=ta.id
										inner join public.seataction sa on (sa.Id=ha.seat1ActionId OR sa.Id=ha.seat2ActionId OR sa.Id=ha.seat3ActionId)
										inner join public.player pa on (sa.playerId = pa.id)
										
									WHERE pa.playernickname = 'IPray2Buddha' and ha.tournamenttype = '3-max' 
									GROUP BY ta.id
									ORDER BY ta.id
								) t1
						GROUP BY (rn - 1)/200
						ORDER BY (rn - 1)/200
					) t2";

        public static string sql_ExportTlpOverviewAsJSON_GeneralInfo_GroupByMonths =
            @"	
set join_collapse_limit = 1;
SELECT array_to_json(array_agg(row_to_json(t2)))
				FROM (
 						SELECT sum (t1.cev) as CEV , sum(t1.Chips_Won) as Amt_Won, avg(t1.aBI) as aBI, count(t1.cev) as Count_Tourney, to_char(min(t1.t_date),'YYYY-MM-DD') as min_Date, to_char(max(t1.t_date),'YYYY-MM-DD') as max_Date
							FROM ( 
									SELECT sum (sa.cev_won) as CEV , sum(sa.chips_won) as Chips_Won , ta.amt_buyin as aBI , ta.tournamentdate as t_date
  									FROM public.tournament ta
										inner join public.hands ha on ha.tournamentid=ta.id
										inner join public.seataction sa on sa.Id=ha.heroseatactionid
										inner join public.player pa on (sa.playerId = pa.id)
										
									WHERE pa.playernickname = 'IPray2Buddha' and ha.tournamenttype = '3-max' 
									GROUP BY ta.id
									ORDER BY ta.id
								) t1
				GROUP BY DATE_TRUNC('month',t_date)
				ORDER BY DATE_TRUNC('month',t_date) DESC
					) t2";


        public static string sql_cevRequestGeneral=
            @"where (pl.PlayerNickName = @activePlayer::varchar AND ha.tournamenttype = @tourneyType::Varchar 
					AND ta.amt_buyin = @amtBI::numeric)";

        public static string sql_cevRequestGeneral_test =
            @"where (pl.PlayerNickName = @activePlayer::varchar AND ha.tournamenttype = @tourneyType::Varchar)";
    }
}
